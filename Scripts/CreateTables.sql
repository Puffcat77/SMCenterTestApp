use MedicDB;

drop table if exists Patient, Doctor, Region, Speciality, Cabinet; 

create table Region
(
	Id int identity(1, 1) primary key, 
	Number int not null
);

create table Speciality
(
	Id int identity(1, 1) primary key,
	Name nvarchar(200) not null
);

create table Cabinet
(
	Id int identity(1, 1) primary key, 
	Number int not null
);

create table Patient
(
	Id uniqueidentifier primary key,
	LastName nvarchar(50) not null,
	FirstName nvarchar(50) not null,
	Surname nvarchar(60) not null,
	Address nvarchar(100) not null,
	BirthDate datetime2 not null,
	Sex bit not null,
	RegionId int references Region(id) not null
);

create table Doctor
(
	Id uniqueidentifier primary key,
	Initials nvarchar(200) not null,
	CabinetId int references Cabinet(id) not null,
	SpecialityId int references Speciality(Id) not null,
	RegionId int references Region(Id) not null
)