import sqlite3


def get_user_data(username):
    # Conexión a la base de datos
    conn = sqlite3.connect("test.db")
    cursor = conn.cursor()

    # Consulta segura usando parámetros
    query = "SELECT * FROM users WHERE username = ?"
    print("Executing query with parameter:", query)

    # Ejecuta la consulta con parámetros seguros
    cursor.execute(query, (username,))
    result = cursor.fetchall()

    # Cierra la conexión
    conn.close()
    return result


if __name__ == "__main__":
    user_input = input("Enter username: ")  # Entrada del usuario
    data = get_user_data(user_input)
    print("User Data:", data)
