CREATE TABLE TeamSpieler (
    TeamID INT NOT NULL,
    SpielerID INT NOT NULL,
    PRIMARY KEY (TeamID, SpielerID),
    FOREIGN KEY (TeamID) REFERENCES Team(id),
    FOREIGN KEY (SpielerID) REFERENCES Spieler(id)
);