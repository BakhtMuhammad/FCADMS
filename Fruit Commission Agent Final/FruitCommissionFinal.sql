Create Database db_FCADMS;
Drop Database db_FCADMS;
Use db_FCADMS;
---------------------------------------------------------------
--1 USER REGISTRATION TABLE
---------------------------------------------------------------
Create Table tb_User
(
 Id int Identity(1,1),
 fullName varchar (30),
 userName varchar (30),
 userPassword varchar (30)
);
Insert into tb_User (fullName, userName, userPassword) Values('Ali','admin', 'admin');
select * from tb_User
---------------------------------------------------------------
--2 FARMER TABLE
---------------------------------------------------------------
Create Table tb_Farmer
( 
Farmer_Id int primary key identity,
    TraderName varchar(255),
    FCNIC varchar(15),
    FMobileNO varchar(30),
	FAddress varchar(50),
	FCity varchar(45),
	FProvince varchar(40),    
);

Select * From tb_Farmer;
Insert into tb_Farmer ( TraderName, FCNIC, FMobileNO, FAddress, FCity, FProvince   )
Values('Ali', '34343434','84784834783', 'quetta','qwerw','bal');
Insert into tb_Farmer ( TraderName, FCNIC, FMobileNO, FAddress, FCity, FProvince   )
Values('Ali', '34343434','84784834783', 'quetta','qwerw','bal');


Update tb_Farmer
Set TraderName='Jon'
Where Farmer_Id =1;
---------------------------------------------------------------
--3 FRUIT TABLE
---------------------------------------------------------------
Create Table tb_Fruit
(
 Id int primary key identity,
    FCNIC varchar(15),
	Date varchar(10),
	FruitName varchar(255),
    StockNO varchar(30),
	SalePerStock varchar(50),
	TradeNo varchar(45)
);

Select * From tb_Fruit;
---------------------------------------------------------------
--4 TRUCK TABLE
---------------------------------------------------------------
Create Table tb_Truck
(
 Id int primary key identity,
	TradeNo varchar(45),
    ArrivalTime varchar(10),
	TruckNumber varchar(25),
   Rent varchar(30)
);
Select * From tb_Truck;
---------------------------------------------------------------
--5 EXPENDITURE TABLE
---------------------------------------------------------------
Create Table tb_Expanditure
(
Id int primary key identity,
	CompanyID varchar(50),
    DriverCNIC varchar(15),
	FarmerCNIC varchar(15),
    Quantity varchar(45),
    TotaleSale varchar(10),
	TruckRent varchar(30),
	TotalCommission varchar(40),
    TotalExpanditure varchar(40)
);
Select * From tb_Expanditure;
---------------------------------------------------------------
--6 NET AMOUNT TABLE
---------------------------------------------------------------
Create Table tb_NetAmount
(
 Id int primary key identity,
	FarmerCNIC varchar(15),
    TotalExpanditure varchar(40),
	NetAmount varchar(40)
);

Select * From tb_NetAmount;

  --<connectionStrings>
  --  <add name="DBConnection" connectionString="Data Source=LAPTOP-Q9CE4QKR\SQLEXPRESS;Initial Catalog=db_FCADMS;Integrated Security=True"/>
  --</connectionStrings>
