--Effacement des tables existantes
IF OBJECT_ID('AnimalProprietaire', 'U') IS NOT NULL DROP TABLE AnimalProprietaire; 
IF OBJECT_ID('Soin', 'U') IS NOT NULL DROP TABLE Soin;
IF OBJECT_ID('Animal', 'U') IS NOT NULL DROP TABLE Animal;

IF OBJECT_ID('Espece', 'U') IS NOT NULL DROP TABLE Espece;
IF OBJECT_ID('Proprietaire', 'U') IS NOT NULL DROP TABLE Proprietaire;
IF OBJECT_ID('Medicament', 'U') IS NOT NULL DROP TABLE Medicament;



create table Espece
( 
ID INT not null IDENTITY(1,1)
	constraint PK_Espece_ID PRIMARY KEY,
Nom VARCHAR(40) NOT NULL
)

create table Animal
( 
ID INT not null IDENTITY(1,1)
	constraint PK_Animal_ID PRIMARY KEY,
Nom VARCHAR(40),
EspeceID INT
    constraint FK_Animaux_EspeceID FOREIGN KEY (EspeceID)
		references Espece(ID)
		ON UPDATE CASCADE,
Couleur VARCHAR(20),
Sexe VARCHAR(1)
	constraint CK_Animal_Sexe check(Sexe in ('M', 'F')),
Poids INT,
DateNaissance DATE
);

create table Proprietaire
(
ID INT not null IDENTITY(1,1)
	constraint PK_Proprietaire_ID PRIMARY KEY,
Nom VARCHAR(40),
Adresse VARCHAR(100)
);

create table Medicament
(
ID INT not null IDENTITY(1,1)
	constraint PK_Medicament_ID PRIMARY KEY,
Nom VARCHAR(40),
DosageUnitaire VARCHAR(100),
PrixUnitaire float
);

create table Soin
(
AnimalID INT 
	constraint FK_Soin_AnimalID FOREIGN KEY (AnimalID)
		references Animal(ID)
		ON UPDATE CASCADE
		ON DELETE CASCADE,
MedicamentID INT 
	constraint FK_Soin_MedicamentID FOREIGN KEY (MedicamentID)
		references Medicament(ID)
		ON UPDATE CASCADE
		ON DELETE CASCADE,
Quantite int

constraint PK_Soin PRIMARY KEY(AnimalID, MedicamentID)
);

create table AnimalProprietaire
(
AnimalID INT 
	constraint FK_AnimalProprietaire_AnimalID FOREIGN KEY (AnimalID)
		references Animal(ID)
		ON UPDATE CASCADE
		ON DELETE CASCADE,
ProprietaireID INT 
	constraint FK_AnimalProprietaire_ProprietaireID FOREIGN KEY (ProprietaireID)
		references Proprietaire(ID)
		ON UPDATE CASCADE
		ON DELETE CASCADE
		
constraint PK_AnimalProprietaire PRIMARY KEY(AnimalID, ProprietaireID)

);


-- Création des données
DELETE FROM AnimalProprietaire
DELETE FROM Soin;
DELETE FROM Animal;
DELETE FROM Espece;
DELETE FROM Medicament;
DELETE FROM Proprietaire;


INSERT INTO Espece (Nom) VALUES 
( 'Cocker' ),
( 'Doberman' ),
( 'Persan'),
( 'Croisement' ),
( 'Commun' ),
( 'Berger Allemand' ),
( 'Berger Belge' ),
( 'Siamois');

INSERT INTO Medicament (Nom, DosageUnitaire, PrixUnitaire) VALUES
( 'Med 1', '1 ml', 1.50)
DECLARE @med1 AS INT; SET @med1 = SCOPE_IDENTITY();
INSERT INTO Medicament (Nom, DosageUnitaire, PrixUnitaire) VALUES
( 'Med 2', '1 cc', 0.35)
DECLARE @med2 AS INT; SET @med2 = SCOPE_IDENTITY();
INSERT INTO Medicament (Nom, DosageUnitaire, PrixUnitaire) VALUES
( 'Med 3', '1 cc', 2.45)
DECLARE @med3 AS INT; SET @med3 = SCOPE_IDENTITY();
INSERT INTO Medicament (Nom, DosageUnitaire, PrixUnitaire) VALUES
( 'Med 4', '2 ml', 3)
DECLARE @med4 AS INT; SET @med4 = SCOPE_IDENTITY();


DECLARE @cocker AS INT;
SET @cocker = (SELECT ID From Espece WHERE Nom = 'Cocker');
 

INSERT INTO Animal (Nom, EspeceID, Couleur, Sexe, Poids, DateNaissance) VALUES
( 'Papoute', @cocker, 'Beige', 'M', 41, '2010-12-24'),
( 'Killer', (SELECT ID From Espece WHERE Nom = 'Persan'), 'Gris', 'M', 13, '2007-10-27'),
( 'Pétasse', (SELECT ID From Espece WHERE Nom = 'Commun'), 'Noir', 'F', 8, '2008-04-10'),
( 'Maya', (SELECT ID  From Espece WHERE Nom = 'Berger Belge'), 'Blanc', 'F', 36, '2010-4-8')

INSERT INTO Soin (AnimalID, MedicamentID, Quantite) VALUES
(  (SELECT ID From Animal WHERE Nom = 'Papoute'), @med1, 3 ),
(  (SELECT ID From Animal WHERE Nom = 'Papoute'), @med2, 2 ), 
(  (SELECT ID From Animal WHERE Nom = 'Killer'), @med2, 1 ), 
(  (SELECT ID From Animal WHERE Nom = 'Killer'), @med3, 2 ), 
(  (SELECT ID From Animal WHERE Nom = 'Killer'), @med4, 4 )


INSERT INTO Proprietaire (Nom, Adresse) VALUES
('Benoit', '123 Rue A'),
('Guylaine', '456 Rue B'),
('George', '999 rue C') 

INSERT INTO AnimalProprietaire (AnimalID, ProprietaireID) VALUES
( (SELECT ID FROM Animal Where Nom = 'Papoute'), (Select ID FROM Proprietaire Where Nom = 'Benoit') ), 
( (SELECT ID FROM Animal Where Nom = 'Papoute'), (Select ID FROM Proprietaire Where Nom = 'Guylaine') ), 
( (SELECT ID FROM Animal Where Nom = 'Killer'), (Select ID FROM Proprietaire Where Nom = 'Benoit') ),
( (SELECT ID FROM Animal Where Nom = 'Pétasse'), (Select ID FROM Proprietaire Where Nom = 'Benoit') ), 
( (SELECT ID FROM Animal Where Nom = 'Maya'), (Select ID FROM Proprietaire Where Nom = 'Guylaine') ) 

