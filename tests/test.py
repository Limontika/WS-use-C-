import unittest
import re

class TestPasswd(unittest.TestCase):
    
    def test_success_passwd(self):
        def_pass = re.compile(r'(?=.*[0-9])(?=.*[!@#$%^&*])(?=.*[a-z])(?=.*[A-Z])[0-9a-zA-Z!@#$%^&*]{6,}')
        success = ['512!Ca', '123!Aa', '333@Cc', '22$2Va', 'V2a@3g2']
        for item in success:
            self.assertTrue(def_pass.search(item))

    def test_unsuccessful_passwd(self):
        def_pass = re.compile(r'(?=.*[0-9])(?=.*[!@#$%^&*])(?=.*[a-z])(?=.*[A-Z])[0-9a-zA-Z!@#$%^&*]{6,}')
        unsuccessful = ['fwef', '12d12', '12d2', 'ff', 'dw21']
        for item in unsuccessful:
            self.assertFalse(def_pass.search(item))

if __name__ == '__main__':
    unittest.main()