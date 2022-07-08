create database bd_japones;
use bd_japones;

create table tbl_Cliente (
cd_Cliente int primary key auto_increment,
nm_Cliente varchar(80) not null,
no_Telefone varchar(11),
login_Cliente varchar(80) not null,
senha_Cliente char(6) not null
);

select * from tbl_Cliente;

drop table tbl_Cliente;

create table tbTipoUsuario(
codTipoUsuario int primary key auto_increment,
usuario varchar(50)
);

insert into tbTipoUsuario values (default, 'admin');
select * from tbTipoUsuario;

create table tbl_Funcionario (
cd_Funcio int primary key auto_increment,
nm_Funcio varchar(80),
login_Funcio varchar(80),
senha_Funcio varchar(5),
codTipoUsuario int,
foreign key (codTipoUsuario) references tbTipoUsuario(codTipoUsuario)
);

select * from tbl_Funcionario;
insert into tbl_Funcionario values (default, 'Henrique', 'Rick', '123', '1');


create table tbl_Categoria (
cd_Cat int primary key auto_increment,
nm_Cat varchar(80) not null
);


create table tbl_prod_cat (
cd_Cat int,
cd_Produto int,
primary key (cd_Cat, cd_Produto),
foreign key (cd_Cat) references tbl_Categoria(cd_Cat),
foreign key (cd_Produto) references tbl_Produto(cd_Produto)
);
select * from tbl_prod_cat;

create table tbl_Produto(
cd_Produto int primary key auto_increment,
nm_Produto varchar(80) not null,
vl_Produto varchar(20) not null,
desc_Produto varchar(140) not null,
qt_Estoque int not null,
img_Produto varchar(255) not null,
cd_Cat int,
foreign key(cd_Cat) references tbl_Categoria(cd_Cat)
);

select * from tbl_Produto;

create table tbVenda(
codVenda int primary key auto_increment,
codCli int references tbCliente(codCli),
datavenda varchar(10),
horavenda varchar(10),
valorFinal varchar(50)
);
drop table tbVenda;
select * from tbVenda;

SELECT * FROM tbVenda ORDER BY codVenda DESC LIMIT 1;
DELIMITER //
Create procedure sp_CodVenda()
begin
SELECT * FROM tbVenda ORDER BY codVenda DESC LIMIT 1;
end //
DELIMITER ;

drop procedure sp_CodVenda;
call sp_CodVenda();

create table itemVenda(
codItemVenda int primary key auto_increment,
codVenda int references tbVenda(codVenda),
codProd  int references tbProduto(codProd),
qtdeVendas int,
valorParcial varchar(20)
);

create table tbl_Pagamento(
cd_Pagto int primary key auto_increment,
ds_Pagto varchar (30) not null
);

create table tbl_prod_cat (
cd_Cat int,
cd_Produto int,
primary key (cd_Cat, cd_Produto),
foreign key (cd_Cat) references tbl_Categoria(cd_Cat),
foreign key (cd_Produto) references tbl_Produto(cd_Produto)
);

create table tbl_Entrega (
cd_entrega int primary key auto_increment,
no_cep varchar(11) not null,
nm_estado varchar(50) not null,
nm_cidade varchar (50) not null,
nm_rua varchar(80) not null,
no_end varchar(5) not null,
nm_complemento varchar(30),
nm_bairro varchar(50) not null,
codVenda int,
foreign key (codVenda) references tbVenda(codVenda)
);
SELECT * FROM tbVenda ;
drop table tbl_Entrega;
SELECT codVenda FROM tbVenda ORDER BY dataVenda DESC LIMIT 1;

create table tbl_Retirada (
cd_Retirada int primary key auto_increment,
hr_Retirada date,
cd_Pedido int,
foreign key (cd_Pedido) references tbl_Pedido(cd_Pedido)
);