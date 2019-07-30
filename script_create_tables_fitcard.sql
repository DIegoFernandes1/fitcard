create table UF(
	idUF int identity(1,1) primary key not null,
	UF varchar(25) not null,
	Sigla char(2) not null
);

create table Cidade(
	idCidade int identity(1,1) primary key not null,
	id_UF int not null,
	Municipio varchar(100) not null

	constraint FK_id_UF foreign key (id_UF) references UF(idUF)
);

create table Endereco(
	idEndereco int identity(1,1) primary key not null,
	id_cidade int not null,
	Logradouro varchar(100) null,

	constraint FK_id_cidade foreign key (id_cidade) references Cidade(idCidade)
);

create table Status(
	idStatus int identity(1,1) primary key not null,
	nomeStatus varchar(15) not null,
);

	insert into Status values('Ativo');
	insert into Status values('Inativo');

create table Categoria(
	idCategoria int identity(1,1) primary key not null,
	nomeCategoria varchar(20) not null
);

	insert into Categoria values('Supermercado');
	insert into Categoria values('Restaurante');
	insert into Categoria values('Borracharia');
	insert into Categoria values('Posto');
	insert into Categoria values('Oficina');

create table Estabelecimento(
	idEstabelecimento int identity(1,1) primary key not null,
	id_Status int not null,
	id_Endereco int not null,
	razaoSocial varchar(50) not null,
	nomeFantasia varchar(50) null,
	CNPJ char(18) not null,
	dataCadastro datetime not null,

	constraint FK_id_status foreign key (id_Status) references Status(idStatus),
	constraint FK_id_endereco foreign key (id_Endereco) references Endereco(idEndereco)
);

create table Estabelecimento_Categoria(
	idEstabelecimentoCategoria int identity(1,1) primary key not null,
	id_Estabelecimento int not null,
	id_Categoria int not null,

	constraint FK_id_estabelecimento foreign key (id_Estabelecimento) references Estabelecimento(idEstabelecimento),
	constraint FK_id_categoria foreign key (id_Categoria) references Categoria(idCategoria)
);

create table Contato(
	idContato int identity(1,1) primary key not null,
	id_estabelecimento int not null,
	Email varchar(50) null,
	DDD char(2) null,
	Numero varchar(9) null

	constraint FK_id_estabelecimento_Contato foreign key (id_estabelecimento) references Estabelecimento(idEstabelecimento)
);

create table Conta(
	idConta int identity(1,1) primary key not null,
	id_Estabelecimento int null,
	Conta char(8) not null,
	Agencia char(5) not null,

	constraint FK_id_estabelecimento_Conta foreign key (id_Estabelecimento) references Estabelecimento(idEstabelecimento)
);


