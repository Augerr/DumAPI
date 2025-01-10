DROP TABLE IF EXISTS dummy_user_right;
DROP TABLE IF EXISTS dummy_right;
DROP TABLE IF EXISTS dummy_user;
DROP TABLE IF EXISTS dummy_role;

CREATE TABLE dummy_right(
	id SERIAL PRIMARY KEY,
	name VARCHAR(255) NOT NULL,
	description VARCHAR(255) NOT NULL
);

CREATE TABLE dummy_role(
	id SERIAL PRIMARY KEY,
	name VARCHAR(255) NOT NULL,
	description VARCHAR(255) NOT NULL
);

CREATE TABLE dummy_user(
	id SERIAL PRIMARY KEY,
	role_id INT NOT NULL,
	first_name VARCHAR(255) NOT NULL,
	last_name VARCHAR(255) NOT NULL,
	username VARCHAR(255) NOT NULL,
	employee_id VARCHAR(10),
	email VARCHAR(255) NOT NULL,
	active boolean NOT NULL,
	created_at TIMESTAMP NOT NULL,
	updated_at TIMESTAMP,
	FOREIGN KEY (role_id) REFERENCES dummy_role(id)
);

CREATE TABLE dummy_user_right(
	id SERIAL PRIMARY KEY,
	user_id INT NOT NULL,
	right_id INT NOT NULL,
	FOREIGN KEY (right_id) REFERENCES dummy_right(id),
	FOREIGN KEY (user_id) REFERENCES dummy_user(id)
);

INSERT INTO dummy_role VALUES (1,'ADMIN', 'Administrator');
INSERT INTO dummy_role VALUES (2,'MOD', 'Moderator');
INSERT INTO dummy_role VALUES (3,'USER', 'Basic user');

INSERT INTO dummy_right VALUES (1,'A1', 'Access A1');
INSERT INTO dummy_right VALUES (2,'A2', 'Access A2');
INSERT INTO dummy_right VALUES (3,'A3', 'Access A3');

INSERT INTO dummy_user VALUES (1,1, 'The', 'Administrator', 'ADM1', 'ADM001', 'adm@ogxz.com', true, '2025-01-01 00:00:00', null);
INSERT INTO dummy_user VALUES (2,2, 'Albert', 'Einstein', 'MOD1', 'MOD001', 'mod@ogxz.com', true, '2025-01-01 00:00:00', null);
INSERT INTO dummy_user VALUES (3,3, 'Mark', 'Smith', 'MSMITH', 'USR001', 'msmith@ogxz.com', true, '2025-01-01 00:00:00', null);

INSERT INTO dummy_user_right VALUES (1,1,1);
INSERT INTO dummy_user_right VALUES (2,1,2);
INSERT INTO dummy_user_right VALUES (3,1,3);
INSERT INTO dummy_user_right VALUES (4,2,1);
INSERT INTO dummy_user_right VALUES (5,2,2);
INSERT INTO dummy_user_right VALUES (6,3,1);

SELECT * FROM dummy_role;
SELECT * FROM dummy_right;
SELECT * FROM dummy_user;
SELECT * FROM dummy_user_right;