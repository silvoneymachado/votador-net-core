CREATE DATABASE alterdata_votador
    WITH 
    OWNER = postgres
    ENCODING = 'UTF8'
    CONNECTION LIMIT = -1;
    
ALTER DATABASE alterdata_votador OWNER TO postgres;

\connect alterdata_votador

CREATE TABLE public.usuario (
	id SERIAL PRIMARY KEY,
	nome VARCHAR(64),
	email VARCHAR(64),
	senha VARCHAR(64),
	isAdmin BOOL DEFAULT '0',
	token character varying(3000)
);

CREATE TABLE public.recurso(
	id SERIAL PRIMARY KEY,
	nome VARCHAR(64),
	descricao VARCHAR(255),
	habilitado BOOL DEFAULT '0'
);

CREATE TABLE public.votacao (
	id SERIAL PRIMARY KEY,
	idUsuario INTEGER REFERENCES Usuario(Id),
	idRecurso INTEGER REFERENCES Recurso(Id),
	dataHora TIMESTAMP,
	domentario VARCHAR(255)
);

ALTER TABLE public.usuario
    OWNER to postgres;

ALTER TABLE public.recurso
    OWNER to postgres;

ALTER TABLE public.votacao
    OWNER to postgres;