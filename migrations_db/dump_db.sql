--
-- PostgreSQL database dump
--

-- Dumped from database version 11.1
-- Dumped by pg_dump version 11.1

-- Started on 2018-12-14 19:00:56

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET client_min_messages = warning;
SET row_security = off;

DROP DATABASE alterdata_votador;
--
-- TOC entry 2197 (class 1262 OID 16554)
-- Name: alterdata_votador; Type: DATABASE; Schema: -; Owner: postgres
--

CREATE DATABASE alterdata_votador WITH TEMPLATE = template0 ENCODING = 'UTF8' LC_COLLATE = 'Portuguese_Brazil.1252' LC_CTYPE = 'Portuguese_Brazil.1252';


ALTER DATABASE alterdata_votador OWNER TO postgres;

\connect alterdata_votador

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET client_min_messages = warning;
SET row_security = off;

SET default_tablespace = '';

SET default_with_oids = false;

--
-- TOC entry 199 (class 1259 OID 16566)
-- Name: recurso; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.recurso (
    id integer NOT NULL,
    nome character varying(64),
    descricao character varying(255),
    habilitado boolean DEFAULT false
);


ALTER TABLE public.recurso OWNER TO postgres;

--
-- TOC entry 198 (class 1259 OID 16564)
-- Name: recurso_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.recurso_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.recurso_id_seq OWNER TO postgres;

--
-- TOC entry 2198 (class 0 OID 0)
-- Dependencies: 198
-- Name: recurso_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.recurso_id_seq OWNED BY public.recurso.id;


--
-- TOC entry 197 (class 1259 OID 16557)
-- Name: usuario; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.usuario (
    id integer NOT NULL,
    nome character varying(64) NOT NULL,
    email character varying(64) NOT NULL,
    senha character varying(64) NOT NULL,
    token character varying(3000),
    roles character varying(255) DEFAULT 'comum'::character varying NOT NULL
);


ALTER TABLE public.usuario OWNER TO postgres;

--
-- TOC entry 196 (class 1259 OID 16555)
-- Name: usuario_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.usuario_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.usuario_id_seq OWNER TO postgres;

--
-- TOC entry 2199 (class 0 OID 0)
-- Dependencies: 196
-- Name: usuario_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.usuario_id_seq OWNED BY public.usuario.id;


--
-- TOC entry 201 (class 1259 OID 16575)
-- Name: votacao; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.votacao (
    id integer NOT NULL,
    idusuario integer,
    idrecurso integer,
    datahora timestamp without time zone,
    comentario character varying(255)
);


ALTER TABLE public.votacao OWNER TO postgres;

--
-- TOC entry 200 (class 1259 OID 16573)
-- Name: votacao_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.votacao_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.votacao_id_seq OWNER TO postgres;

--
-- TOC entry 2200 (class 0 OID 0)
-- Dependencies: 200
-- Name: votacao_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.votacao_id_seq OWNED BY public.votacao.id;


--
-- TOC entry 2054 (class 2604 OID 16569)
-- Name: recurso id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.recurso ALTER COLUMN id SET DEFAULT nextval('public.recurso_id_seq'::regclass);


--
-- TOC entry 2052 (class 2604 OID 16560)
-- Name: usuario id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.usuario ALTER COLUMN id SET DEFAULT nextval('public.usuario_id_seq'::regclass);


--
-- TOC entry 2056 (class 2604 OID 16578)
-- Name: votacao id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.votacao ALTER COLUMN id SET DEFAULT nextval('public.votacao_id_seq'::regclass);


--
-- TOC entry 2189 (class 0 OID 16566)
-- Dependencies: 199
-- Data for Name: recurso; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public.recurso (id, nome, descricao, habilitado) VALUES (2, 'PDV Legal', 'Ponto de venda para pequenas e medias empresas', true);
INSERT INTO public.recurso (id, nome, descricao, habilitado) VALUES (3, 'Gestor de RH', 'Gestor de recursos humanos, agendamentos, etc', true);
INSERT INTO public.recurso (id, nome, descricao, habilitado) VALUES (4, 'Aplicativo de agendamento', 'Aplicativo generico de agendamentos', true);
INSERT INTO public.recurso (id, nome, descricao, habilitado) VALUES (1, 'ERP Bacana', 'ERP para controle fabril', true);


--
-- TOC entry 2187 (class 0 OID 16557)
-- Dependencies: 197
-- Data for Name: usuario; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public.usuario (id, nome, email, senha, token, roles) VALUES (9, 'fulano', 'fulano@alterdata.com', '8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92', NULL, 'comum');
INSERT INTO public.usuario (id, nome, email, senha, token, roles) VALUES (10, 'ciclano', 'ciclano@alterdata.com', '8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92', NULL, 'comum');
INSERT INTO public.usuario (id, nome, email, senha, token, roles) VALUES (11, 'beltrano', 'beltrano@alterdata.com', '8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92', NULL, 'comum');
INSERT INTO public.usuario (id, nome, email, senha, token, roles) VALUES (1, 'admin', 'adminvotacao@alterdata.com.br', '6ce1a906a9bd0c097ccba160b55f92536ef08d741b9f861e8e291fd90996aa90', 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjEiLCJuYmYiOjE1NDQ3NTI2NTcsImV4cCI6MTU0NTM1NzQ1NywiaWF0IjoxNTQ0NzUyNjU3fQ.-ZbZNbsL2HDoWCV7UUems6kwZAxY6i8c6ytXWV7DM1M', 'rh');
INSERT INTO public.usuario (id, nome, email, senha, token, roles) VALUES (8, 'teste', 'teste@alterdata.com', '8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92', NULL, 'controle_producao');


--
-- TOC entry 2191 (class 0 OID 16575)
-- Dependencies: 201
-- Data for Name: votacao; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public.votacao (id, idusuario, idrecurso, datahora, comentario) VALUES (2, 10, 4, '2018-12-14 14:38:38.353251', 'interessante');
INSERT INTO public.votacao (id, idusuario, idrecurso, datahora, comentario) VALUES (1, 9, 4, '2018-12-14 14:38:38.353251', 'prioridade na minha concepção');
INSERT INTO public.votacao (id, idusuario, idrecurso, datahora, comentario) VALUES (3, 1, 1, '2018-12-14 14:47:09.551638', 'legal');
INSERT INTO public.votacao (id, idusuario, idrecurso, datahora, comentario) VALUES (4, 11, 4, '2018-12-14 14:49:49.247227', 'legal');
INSERT INTO public.votacao (id, idusuario, idrecurso, datahora, comentario) VALUES (5, 8, 1, '2018-12-14 14:50:35.364083', 'tranquilo');
INSERT INTO public.votacao (id, idusuario, idrecurso, datahora, comentario) VALUES (6, 8, 3, '2018-12-14 14:51:40.667797', 'tranquilo');


--
-- TOC entry 2201 (class 0 OID 0)
-- Dependencies: 198
-- Name: recurso_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.recurso_id_seq', 4, true);


--
-- TOC entry 2202 (class 0 OID 0)
-- Dependencies: 196
-- Name: usuario_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.usuario_id_seq', 11, true);


--
-- TOC entry 2203 (class 0 OID 0)
-- Dependencies: 200
-- Name: votacao_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.votacao_id_seq', 6, true);


--
-- TOC entry 2060 (class 2606 OID 16572)
-- Name: recurso recurso_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.recurso
    ADD CONSTRAINT recurso_pkey PRIMARY KEY (id);


--
-- TOC entry 2058 (class 2606 OID 16563)
-- Name: usuario usuario_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.usuario
    ADD CONSTRAINT usuario_pkey PRIMARY KEY (id);


--
-- TOC entry 2062 (class 2606 OID 16580)
-- Name: votacao votacao_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.votacao
    ADD CONSTRAINT votacao_pkey PRIMARY KEY (id);


--
-- TOC entry 2064 (class 2606 OID 16586)
-- Name: votacao votacao_idrecurso_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.votacao
    ADD CONSTRAINT votacao_idrecurso_fkey FOREIGN KEY (idrecurso) REFERENCES public.recurso(id);


--
-- TOC entry 2063 (class 2606 OID 16581)
-- Name: votacao votacao_idusuario_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.votacao
    ADD CONSTRAINT votacao_idusuario_fkey FOREIGN KEY (idusuario) REFERENCES public.usuario(id);


-- Completed on 2018-12-14 19:00:58

--
-- PostgreSQL database dump complete
--

