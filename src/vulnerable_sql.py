import sqlite3


def get_user_data(username):
    # Conexión a la base de datos
    conn = sqlite3.connect("test.db")
    cursor = conn.cursor()

    # Consulta SQL vulnerable (concatenación de variables directamente en la consulta)
    query = f"SELECT * FROM users WHERE username = '{username}'"
    print("Executing query:", query)

    # Ejecuta la consulta
    cursor.execute(query)
    result = cursor.fetchall()

    # Cierra la conexión
    conn.close()
    return result


if __name__ == "__main__":
    user_input = input("Enter username: ")  # Entrada del usuario
    data = get_user_data(user_input)
    print("User Data:", data)
