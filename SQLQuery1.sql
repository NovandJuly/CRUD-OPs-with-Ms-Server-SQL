CREATE TABLE clients (
		id INT NOT NULL PRIMARY KEY IDENTITY,
		firstname VARCHAR (100) NOT NULL,
		lastname VARCHAR (100) NOT NULL,
		email VARCHAR (100) NOT NULL,
		phone VARCHAR (100) NOT NULL,
		address VARCHAR (200) NOT NULL,
		created_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP
);

INSERT INTO clients (firstname, lastname, email, phone, address)
VALUES 
('Ahmed', 'Hani', 'A.Hani@outlook.com', '07-75-65-98-77', '29 CA CAIRO')