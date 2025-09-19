USE Gruempeli;

CREATE TABLE Spielplan (
    SpielNr     INT IDENTITY(1,1) PRIMARY KEY,
    Team1ID     INT NOT NULL,
    Team2ID     INT NOT NULL,
    Spieldauer  INT NOT NULL,
    PausenDauer INT NOT NULL,
    Startzeit   DATETIME2(0) NOT NULL
);