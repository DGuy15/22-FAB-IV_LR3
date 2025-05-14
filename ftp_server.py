from pyftpdlib.handlers import FTPHandler
from pyftpdlib.servers import FTPServer
from pyftpdlib.authorizers import DummyAuthorizer

# Создаём авторизатор и добавляем пользователя
authorizer = DummyAuthorizer()
authorizer.add_user("user", "12345", "E:", perm="elradfmwMT")  # доступ к текущей папке

# Настраиваем обработчик и передаём авторизатор
handler = FTPHandler
handler.authorizer = authorizer

# Создаём и запускаем сервер
server = FTPServer(("0.0.0.0", 21), handler)
print("FTP-сервер запущен на порту 21. Логин: user, Пароль: 12345")
server.serve_forever()
