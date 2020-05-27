--Denise, Jacob, Kelsey, please execute lines 3 and 4

--create database CandyMarket

--use CandyMarket

CREATE TABLE Candy(
ID int primary key identity (1,1) not null, --"identity" that does an automated increment for you, so you don't have to insert a value for ID ON EVERY CANDY THAT'S CREATED
[Name] varchar(50) not null,
Flavor varchar(50) not null,
Manufacturer varchar(50) not null,
ExpirationDate datetime not null
);

create table [User](
ID int primary key identity (1,1) not null,
[Name] varchar(50) not null, 
Email varchar(50) not null
);

create table OwnersCandy(
ID int primary key identity (1,1) not null,
DateReceived datetime not null, 
UserId int foreign key references [User](ID), 
CandyId int foreign key references Candy(ID), 
IsEaten bit default 0,
EatenDate datetime null
);

insert into Candy ([Name],Flavor,Manufacturer,ExpirationDate)
values('Snickers','Chocolate','Mars','2021-12-12' ),
('Peanut Butter Cups','Peanut Butter','Justins', '2021-12-12'),
('Damla','Fruity','Turkish', '2021-12-12'),
('3 Musketeers','Chocolate','Mars','2021-12-12' ),
('Reeses','Peanut Butter','Justins', '2021-12-12'),
('Skittles','Fruity','Mars', '2021-12-12'),
('Weed Gummies','Fruity','Willie Nelson','2069-04-20' ),
('Zagnut','Peanut Butter','Hershey', '2020-08-17'),
('Mike and Ikes','Fruity','Just Born', '2020-07-05'),
('Placebo','Fruity','Control Group', '2020-05-05'),
('Placebo','Fruity','Control Group', '2020-05-07'),
('Placebo','Chocolate','Control Group', '2020-05-05'),
('Placebo','Peanut Butter','Control Group', '2020-05-05');


insert into [User] ([Name],Email)
values('Emilee','emilee@candy.com'),('Jacob','Jacob@candy.com'),('Denise','Denise@candy.com'),('Kelsey','Kelsey@candy.com'), ('Cheech', 'weed@420.com');

--Emilee = 1
--Jacob = 2
--Denise = 3
--Kels = 4

insert into OwnersCandy (DateReceived,UserId,CandyId)
values('2014-03-24', 1, 4),
('2004-05-19', 2, 5),
('2020-11-19', 3, 6),
('2015-10-31', 1, 7),
('2017-04-20', 2, 8),
('2014-03-24', 3, 9),
('2004-05-19', 4, 10),
('2020-11-19', 1, 11),
('2015-10-31', 2, 12),
('2017-04-20', 3, 13)
--everyone on my team will use this same script to create their DB