create database ImportExportDb
use ImportExportDb

------------------------------------------------------------------

create table tbl_import   (
userId  int primary key ,
	 Name  varchar(255) not null ,
	   surname    varchar(255) not null,
	   address  varchar(255) not null,
);

-----------------------------------------------------------------------
select * from tbl_import