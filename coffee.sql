create database Coffee;
use Coffee;
create table Cashiers(
cashier_id int primary key not null auto_increment,
userName char(200) not null ,
pass char(200) not null,
cashier_name char(200) not null,
address char(50),
phone char(20),
role int not null default 1
);
drop table Cashiers;
select * from Cashiers;
update Cashiers set userName='hapf12' where cashier_id = 2;
insert into Cashiers(userName,pass,cashier_name,address,phone,role)
values ('tuanpf12','7692d338c96e22f74912d79dc5ec3b63','Nguyễn Văn Tuân','Ninh Bình','0868996040',1);
create user if not exists 'vtca_pf12'@'localhost' identified by 'tuan2001';
grant all on Coffee.* to 'vtca_pf12'@'localhost';

drop table Cashiers;
select * from Cashiers;
create table Items
(
item_id int primary key not null auto_increment,
item_name char(100) not null,
item_price double not null ,
item_quantity int not null,
item_description char(100)
);
Insert into Items(item_name,item_price,item_quantity,item_description)
value ('Coffee Ice Black',20000,100,'good'),
('Milk Coffee',20000,100,'noproblem'),
('Milk Tea',20000,100,'sweet');
select * from Items where item_id = 1;

select item_name as Name from Items where item_name like '%Milk%';
drop table Items;
insert into Items(item_id,item_name,item_price,item_quantity,item_description)
value(1,"Black Ice Coffee",20000,100,"VeryGood"),
(2,"Milk Coffee",20000,100,"VeryGood");
Alter Table Items add constraint check_price check (item_price > 0);
Alter Table Items add constraint check_quantity check (item_quantity > 0);
create table Coffee_Tables(
table_number int primary key not null,
seats int
);
create table Orders(
order_date date,
table_number int , 
total double,
order_status char(100)
);

Alter table Orders add constraint fk_table foreign key (table_number) references Coffee_Tables(table_number);