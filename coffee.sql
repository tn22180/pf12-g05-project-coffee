create database Coffee;
use Coffee;
create table Cashiers(
cashier_id int primary key auto_increment,
userName char(200) not null ,
pass char(200) not null,
cashier_name char(200) not null,
address char(50),
phone char(20),
role int not null default 1
);

drop table Cashiers;

select * from Cashiers;

update Cashiers set phone='0123467891' where cashier_id = 2;
insert into Cashiers(userName,pass,cashier_name,address,phone,role)
values ('tuanpf12','7692d338c96e22f74912d79dc5ec3b63','Nguyễn Văn Tuân','Ninh Bình','0868996040',1);
create user if not exists 'vtca_pf12'@'localhost' identified by 'tuan2001';
grant all on Coffee.* to 'vtca_pf12'@'localhost';

drop table Cashiers;
select * from Cashiers;
create table Items
(
item_id int primary key auto_increment,
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
table_number int primary key not null auto_increment,
table_status int  not null default 1
);
insert into Coffee_Tables(table_status)
values(1),(1),(1),(1),(1),(1);
update Coffee_Tables set table_status = 1 where table_number = 6;
select * from Coffee_Tables;

drop table Coffee_Tables;
select * from Coffee_Tables;
select order_date from Orders where order_id = 5;
create table Orders(
order_id int primary key auto_increment,
cashier_id int not null,
table_number int not null, 
order_date datetime default now() not null,
order_status int not null,
constraint fk_cashier_id foreign key (cashier_id) references Cashiers(cashier_id)
);
drop table Orders;
select order_id from Orders order by order_id desc limit 1;
Alter table Orders Add cashier_id int not null;
select * from Orders;

Alter table Orders add constraint fk_table foreign key (table_number) references Coffee_Tables(table_number);
ALTER TABLE Orders
Drop foreign key fk_cashier_id;
ALTER TABLE Orders
Drop foreign key fk_table;
insert into Orders (table_number, cashier_id, order_status) values (1,2,1);
select * from Orders;
select * from Coffee_Tables;

create table OrderDetails(
	order_id int not null,
    item_id int not null,
    item_price double not null,
    quantity int not null,
    constraint pk_OrderDetails primary key(order_id, item_id),
    constraint fk_OrderDetails_Orders foreign key(order_id) references Orders(order_id),
    constraint fk_OrderDetails_Items foreign key(item_id) references Items(item_id)
    );
    drop table OrderDetails;
    ALTER TABLE OrderDetails
	Drop foreign key fk_OrderDetails_Orders;
    ALTER TABLE OrderDetails ADD constraint fk_OrderDetails_Orders foreign key(order_id) references Orders(order_id);
    insert into Items(item_name,item_price,item_quantity,item_description)
value("Brown Coffee",20000,10,"");
    delimiter $$
create trigger tg_before_insert before insert
	on Items for each row
    begin
		if new.item_quantity < 0 then
            signal sqlstate '45001' set message_text = 'tg_before_insert: amount must > 0';
        end if;
    end $$
delimiter ;

delimiter $$
create trigger tg_CheckAmount
	before update on Items
	for each row
	begin
		if new.item_quantity < 0 then
            signal sqlstate '45001' set message_text = 'tg_CheckAmount: amount must > 0';
        end if;
    end $$
delimiter ;
select * from Orders;
    select * from OrderDetails;
    select last_insert_id() as order_id;
    select table_number from Coffee_Tables order by table_number desc limit 1;
    select table_number from Coffee_Tables where table_status = 1;
    select table_number from Coffee_Tables where table_status = 1 and table_number = 4;
    update Coffee_Tables set table_status = @number where table_number = "+order.table+";