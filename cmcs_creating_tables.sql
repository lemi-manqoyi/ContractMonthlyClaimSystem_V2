create table Users(
UserID  uniqueidentifier default newID() primary key,
Name varchar(155) not null,
Surname varchar(150) not null,
Role varchar(100) not null,
Email varchar(150) not null,
Password varchar(150) not null, 
);

create table Claims(
ClaimID  uniqueidentifier default newID() primary key,
SubmittedDate datetime,
HoursWorked int,
HourlyRate decimal,
ClaimDocumentPath varchar(255),
Status varchar(50) default 'Pending',
UserID  uniqueidentifier not null,
constraint fk_claim_user foreign key (UserID) references Users
);