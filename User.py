import hashlib


class User:
    def __init__(self, login, role_id, role_title=None, user_id=None, password=None):
        self.user_id = user_id
        self.login = login
        self.role_id = role_id
        self.role_title = role_title

        if password is not None:
            self.set_password(password)
        else:
            self.password = None

    def get_data(self):
        if self.user_id is None:
            if self.password is not None:
                return self.login, self.password, self.role_id
            else:
                raise Exception('To create new user set password!')

        return {
            'id': self.user_id,
            'login': self.login,
            'role': {
                'id': self.role_id,
                'title': self.role_title
            }
        }

    def set_password(self, password, hashed=False):
        if not hashed:
            password = hashlib.sha1(bytearray(password, 'utf-8')).hexdigest()

        self.password = password
