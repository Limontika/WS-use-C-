import time

try:
    import sqlite3
    import hashlib
    from User import User
    from database.Requests import requests
except ImportError as error:
    print(error)
    exit(1)


class DatabaseController:
    def __init__(self, db_name: str = 'database/new.db'):
        """
        Инициализация контроллера базы данных
        """
        self.db_name = db_name
        self.connect = sqlite3.connect(db_name)

    def get_roles(self):
        """
        Возвращает все роли

        :return: List of Tuples
        """
        cursor = self.connect.cursor()

        cursor.execute(requests['roles']['select_all_roles'])
        roles = cursor.fetchall()

        return roles

    def get_user(self, login: str, password: str, hashed: bool = False):
        """
        Возвращает класс User по логину и паролю
        Возвращает False, если пользователь не найден

        :return: User or False
        """
        cursor = self.connect.cursor()

        if not hashed:
            password = hashlib.sha1(bytearray(password, 'utf-8')).hexdigest()

        cursor.execute(requests['users']['select_user'], (login, password))
        user = cursor.fetchone()

        if user is None:
            return False

        cursor.execute(requests['roles']['select_role'], (user[3],))
        role = cursor.fetchone()
        user = User(user_id=user[0], login=user[1], role_id=role[0], role_title=role[1])

        return user

    def get_user_by_id(self, id):
        """
        Возвращает класс User по id
        Возвращает False, если пользователь не найден

        :return: User or False
        """
        cursor = self.connect.cursor()

        user = cursor.execute(requests['users']['select_user_by_id'], (id,)).fetchone()

        if user is None:
            return False

        cursor.execute(requests['roles']['select_role'], (user[3],))
        role = cursor.fetchone()
        user = User(user_id=user[0], login=user[1], role_id=role[0], role_title=role[1])

        return user

    def insert_user(self, user: User):
        """
        Вносит нового пользователя в базу данных
        :return: False or User
        """
        cursor = self.connect.cursor()

        try:
            cursor.execute(requests['users']['insert_user'], user.get_data())
        except sqlite3.Error as Error:
            print('Insert user error', Error)
            return False
        finally:
            user_id = cursor.lastrowid
            self.connect.commit()

            return self.get_user_by_id(user_id)

    def get_products(self):
        """
        Возвращает все изделия из базы данных

        :return: List of Tuples (vendor_code, name, quantity, width, height, unit_id)
        """
        cursor = self.connect.cursor()

        cursor.execute(requests['products']['select_all_products'])
        products = cursor.fetchall()

        return products

    def get_findings(self):
        """
        Возвращает всю фурнитуру

        :return: List of Tuples (id, title, quantity)
        """
        cursor = self.connect.cursor()

        cursor.execute(requests['findings']['select_all_findings'])
        findings = cursor.fetchall()

        return findings

    def get_rolls(self):
        """
        Возвращает рулоны ткани

        :return: List of Tuples (vendor_code, composition, color, width, height)
        """
        cursor = self.connect.cursor()

        cursor.execute(requests['rolls']['select_all_rolls'])
        rolls = cursor.fetchall()

        return rolls

    def get_orders(self):
        """
        Возвращает список всех заказов
        """

        cursor = self.connect.cursor()
        # TODO move to requests
        cursor.execute('SELECT id, state, customer_id, manager_id, created_at, updated_at FROM orders')
        orders = cursor.fetchall()

        return orders

    def get_order(self, order_id):
        """
        Возвращает заказ по id
        """

        cursor = self.connect.cursor()
        # TODO move to requests
        cursor.execute('SELECT id, state, customer_id, manager_id, created_at, updated_at FROM orders '
                       'WHERE id=?', (order_id,))

        return cursor.fetchone()

    def get_products_from_orders(self, order_id):
        """
        Возвращает все изделия прикрепленное к заказу
        """

        cursor = self.connect.cursor()

        cursor.execute(requests['orders']['select_products_in_order'], (order_id,))

        products_from_order = cursor.fetchall()
        products_id = [(i[1],) for i in products_from_order]

        products = [cursor.execute(requests['products']['select_product'], product_id).fetchall()
                    for product_id in products_id]

        return products

    def insert_new_order(self, user_id: int, products: list):
        """
        Вносит новый заказ в БД
        products должен иметь вид: [(product_id, quantity),(product_id, quantity),...]
        """

        cursor = self.connect.cursor()

        now = time.strftime("%d-%m-%Y %H:%M:%S", time.gmtime())
        order = ('Новый', user_id, None, now, now)
        cursor.execute(requests['orders']['insert_order'], order)
        order_id = cursor.lastrowid
        self.connect.commit()

        products = [(order_id,) + product for product in products]
        # TODO Тут мне надоело переносить реквесты в отдельный файл, как-нибудь потом сделаю
        cursor.executemany('INSERT INTO orders_has_products (orders_id, products_vendor_code, quantity)'
                           'VALUES (?, ?, ?)', products)

        return order_id

    def get_orders_without_managers(self):
        """
        Возвращает все заказы без менеджера
        """

        cursor = self.connect.cursor()
        # TODO move to requests
        orders = cursor.execute('SELECT id, state, customer_id, manager_id, created_at, updated_at FROM orders '
                                'WHERE manager_id IS NULL').fetchall()

        return orders

    def set_manager_order(self, order_id, manager_id):
        """
        Устанавливает менеджера в заказ
        """

        cursor = self.connect.cursor()
        now = time.strftime("%d-%m-%Y %H:%M:%S", time.gmtime())
        user = self.get_user_by_id(manager_id)

        if user:
            return False

        # TODO move to requests
        cursor.execute('UPDATE orders SET manager_id=?, updated_at=?, state=?'
                       'WHERE id=?', (manager_id, now, 'Установлен менеджер {}'.format(user.login), order_id))
        self.connect.commit()

        return True

    def update_order_status(self, order_id: int, status: str):
        """
        Обновляет статус у заказа
        """

        cursor = self.connect.cursor()

        try:
            now = time.strftime("%d-%m-%Y %H:%M:%S", time.gmtime())
            # TODO: move to requests
            cursor.execute('UPDATE orders SET state=?, updated_at=? WHERE id=?', (status, now, order_id))
        except sqlite3.DatabaseError as error:
            print(error)
            return False
        finally:
            self.connect.commit()
            return True

    def download_items_report(self, path="./reports/"):
        """
        Сохраняет отчет по остатку на диск в .CSV
        """

        now = time.gmtime()

        try:
            file_path = path + time.strftime("%d-%m-%Y_%H%M%S", now) + '.csv'
            f = open(file_path, 'x')
        except FileExistsError:
            raise Exception('Ошибка! Файл уже существует.')

        cursor = self.connect.cursor()

        cursor.execute('SELECT vendor_code, composition, color, width, height FROM rolls')

        rolls = cursor.fetchall()

        info = 'Выгрузка остатков материалов от ' + time.strftime("%d-%m-%Y %H:%M:%S", now)
        title = 'composition;color;width;height'
        f.write(info + '\n')
        f.write(title + '\n')
        [f.write(';'.join([str(i) for i in row[1:]]) + '\n') for row in rolls]
        f.close()

        return True


if __name__ == '__main__':
    db = DatabaseController('new.db')
    # print(db.get_products_from_orders(3))
    # products = [(1, 756), (11, 16), (24, 34), (1,4)]
    # order_id = db.insert_new_order(3, products)
    # print(db.get_products_from_orders(order_id))
    # time.strftime("%d-%m-%Y %H:%M:%S", time.gmtime())
    print(db.download_items_report())

    # order_id = 4
    # print(db.update_order_status(order_id, 'Новый статус'))
