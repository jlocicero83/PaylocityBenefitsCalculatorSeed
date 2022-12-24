use master 
GO

Drop Database if exists PctyBenefitsCalc_JL

Create Database PctyBenefitsCalc_JL
GO

use PctyBenefitsCalc_JL
GO

create table Employee (
Id int identity primary key,
FirstName nvarchar(50),
LastName nvarchar(50),
Salary money,
DateOfBirth date,
)

create table Relationship (
Id int identity primary key,
RelationshipType nvarchar(30)
)

create table Dependent (
Id int identity primary key,
FirstName nvarchar(50),
LastName nvarchar(50),
DateOfBirth date,
RelatedEmployeeId int,
RelationshipId int
constraint fk_Dependent_Employee Foreign Key (RelatedEmployeeId) References Employee(Id),
constraint fk_Dependent_Relationship Foreign Key (RelationshipId) References Relationship(Id)
)



--*****TEST DATA********
insert into Employee(FirstName, LastName, Salary, DateOfBirth)
	values('LeBron', 'James', 75420.99, '1984-12-30'),
		  ('Ja', 'Morant', 92365.22, '1999-08-10'),
		  ('Michael', 'Jordan', 143211.12, '1963-02-17')

insert into Relationship(RelationshipType)
	values('Spouse'),
		  ('Domestic Partner'),
		  ('Child')

insert into Dependent(FirstName, LastName, DateOfBirth, RelatedEmployeeId, RelationshipId)
	values('Spouse', 'Morant', '1998-03-03', (select Id from Employee where LastName = 'Morant'), (select Id from Relationship where RelationshipType = 'Spouse')),
		  ('Child1', 'Morant', '2020-06-23', (select Id from Employee where LastName = 'Morant'), (select Id from Relationship where RelationshipType = 'Child')),
		  ('Child2', 'Morant', '2021-05-18', (select Id from Employee where LastName = 'Morant'), (select Id from Relationship where RelationshipType = 'Child')),
		  ('DP', 'Jordan', '1974-01-02', (select Id from Employee where LastName = 'Jordan'), (select Id from Relationship where RelationshipType = 'Domestic Partner'))
