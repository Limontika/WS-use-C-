requests = {
    'users': {
        'select_all_users': 'SELECT id, login, password, role_id FROM users',
        'select_user': 'SELECT id, login, password, role_id FROM users WHERE login=? AND password=?',
        'select_user_by_id': 'SELECT id, login, password, role_id FROM users WHERE id=?',
        'insert_user': 'INSERT INTO users (login, password, role_id) VALUES (?, ?, ?)',
        'select_role': 'SELECT id, title FROM roles WHERE id=?',
    },
    'roles': {
        'select_all_roles': 'SELECT id, title FROM roles',
        'select_role': 'SELECT id, title FROM roles WHERE id=?',
    },
    'units': {
        'select_all_units': 'SELECT id, name, short_name FROM units',
    },
    'products': {
        'select_all_products': 'SELECT vendor_code, name, quantity, width, height, unit_id FROM products',
        'select_product': 'SELECT vendor_code, name, quantity, width, height, cost, unit_id '
                          'FROM products WHERE vendor_code=?'
    },
    'rolls': {
        'select_all_rolls': 'SELECT vendor_code, composition, color, width, height FROM rolls'
    },
    'findings': {
        'select_all_findings': 'SELECT id, title, quantity FROM findings'
    },
    'orders': {
        'select_all_orders': 'SELECT id, state, customer_id, manager_id, created_at, updated_at FROM orders',
        'select_orders_where_manager': 'SELECT id, state, customer_id, manager_id, created_at, updated_at '
                                       'FROM orders WHERE manager_id=?',
        'select_products_in_order': 'SELECT orders_id, products_vendor_code, quantity '
                                    'FROM orders_has_products WHERE orders_id=?',
        'insert_order': 'INSERT INTO orders(state,customer_id,manager_id,created_at,updated_at) '
                        'VALUES (?,?,?,?,?)'
    }

}
