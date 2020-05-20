--Denise, Jacob, Kelsey, please execute lines 3 and 4

 --create database CandyMarket

--use CandyMarket

CREATE TABLE Candy (
ID int primary key identity (1,1) not null, --"identity" that does an automated increment for you, so you don't have to insert a value for ID ON EVERY CANDY THAT'S CREATED
[Name] varchar(50) not null,
Flavor varchar(50) not null,
Manufacturer varchar(50) not null,
ExpirationDate datetime not null
);

create table Owner(
ID int primary key identity (1,1) not null,
[Name] varchar(50) not null, 
Email varchar(50) not null
);

create table OwnersCandy(
ID int primary key identity (1,1) not null,
DateReceived datetime not null, 
OwnerId int not null, 
CandyId int not null,
IsEaten bit default 0,
EatenDate datetime null
);

insert into Candy ([Name],Flavor,Manufacturer,ExpirationDate)
values('Snickers','Chocolate','Mars','2021-12-12' ),
('Peanut Butter Cups','Peanut Butter','Justins', '2021-12-12'),
('Damla','Fruity','Turkish', '2021-12-12');


insert into [Owner] ([Name],Email)
values('Emilee','emilee@candy.com'),('Jacob','Jacob@candy.com'),('Denise','Denise@candy.com'),('Kelsey','Kelsey@candy.com');


insert into OwnersCandy (DateReceived, OwnerId, CandyId)
values('2020-05-01',1,1),
('2020-05-19',2,3),
('2020-12-21',3,2),
('2020-12-05',3,4),
('1984-09-27',2,3)

--everyone on my team will use this same script to create their DB