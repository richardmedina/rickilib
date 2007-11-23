# Archivo de definicion de base de datos para Proyecto SCS
# mysql -u root -p < sco.sql



DROP DATABASE IF EXISTS scs;

CREATE DATABASE scs;

use scs;

CREATE TABLE Obras (
	id int not null auto_increment,
	nombre char (255),
	fecha_inicio date,
	fecha_termino date,
	empresa_id int,
	saldo_inicial double,
	prorroga date,
	PRIMARY KEY (id)
) type = InnoDb;


CREATE TABLE Companies (
	id int not null auto_increment,
	nombre char (255),
	PRIMARY KEY (id)
) type = InnoDb;


CREATE TABLE Empleados (
	id int not null auto_increment,
	puesto_id int,
	nombre varchar(100),
	imss_id double,
	PRIMARY KEY (id)
) type = InnoDb;

CREATE TABLE Departamentos (
	id int not null auto_increment,
	nombre char (50),
	PRIMARY KEY (id)
) type = InnoDb;;

CREATE TABLE Puestos (
	id int not null auto_increment,
	name char (255),
	salario float,
	PRIMARY KEY (id)
);

# Dudoso ..pendiente
#
#CREATE TABLE Herramientas (
#	int id not null auto_increment,
#	nombre char (50),
#	descripcion text,
#	PRIMARY KEY (id)
#) type = InnoDb;;

CREATE TABLE ValesGasolina (
	id int not null auto_increment,
	obra_id int,
	cantidad double,
	folio int,
	kilometraje double,
	fecha date,
	empleado_id int,
	PRIMARY KEY (id)
) type = InnoDb;;

# Almacen Temporal
CREATE TABLE ElementosAlmacen (
	id int not null auto_increment,
	tipo int,
	cantidad int,
	nombre varchar (255),
	descripcion varchar (255),
	precio double,
	prestados int,
	PRIMARY KEY (id)
) type = InnoDb;


