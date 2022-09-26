create database Adocao_ONG;

use Adocao_ONG;

create table Pessoa (
Cpf varchar(11) not null primary key,
Nome_Pes varchar(50) not null,
Sexo char(1) not null,
Data_Nascimento varchar(8) not null,
Telefone varchar(14) not null,
Logradouro varchar(50) not null,
Numero int not null,
Bairro varchar(50) not null,
Cidade varchar(50) not null,
Estado varchar(50) not null,
Cep varchar(50) not null
);

create table Animal (
Chip int identity primary key,
Familia varchar(50) not null,
Raca varchar(50) not null,
Sexo char(1) not null,
Nome_Ani varchar(50) null,
);

create table Adocao (
Cpf varchar(11) foreign key references Pessoa(Cpf) not null,
Chip int foreign key references Animal(Chip) not null
);

create table AnimaisDisponiveis (
Chip int foreign key references Animal(Chip),
primary key (Chip) 
);

alter table Adocao add constraint PK_Adocao primary key (Cpf, Chip);

select * from Pessoa
select * from Animal
select * from Adocao
select * from AnimaisDisponiveis