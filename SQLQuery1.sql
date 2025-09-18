CREATE TABLE Spieler (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Vorname VARCHAR(100) NOT NULL,
    Name VARCHAR(100) NOT NULL,
    Geburtsdatum DATE NOT NULL,
    Wohnort VARCHAR(100) NOT NULL
);

INSERT INTO Spieler (Vorname, Name, Geburtsdatum, Wohnort)
VALUES ('Max', 'Mustermann', '2000-01-01', 'Zürich');

SELECT * FROM Spieler
