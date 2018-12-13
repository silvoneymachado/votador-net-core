--
-- PostgreSQL database dump
--

-- Dumped from database version 11.1
-- Dumped by pg_dump version 11.1

-- Started on 2018-12-12 21:15:12

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET client_min_messages = warning;
SET row_security = off;

--
-- TOC entry 2196 (class 1262 OID 16554)
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
-- TOC entry 2197 (class 0 OID 0)
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
    nome character varying(64),
    email character varying(64),
    senha character varying(64),
    isadmin boolean DEFAULT false
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
-- TOC entry 2198 (class 0 OID 0)
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
    domentario character varying(255)
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
-- TOC entry 2199 (class 0 OID 0)
-- Dependencies: 200
-- Name: votacao_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.votacao_id_seq OWNED BY public.votacao.id;


--
-- TOC entry 2053 (class 2604 OID 16569)
-- Name: recurso id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.recurso ALTER COLUMN id SET DEFAULT nextval('public.recurso_id_seq'::regclass);


--
-- TOC entry 2051 (class 2604 OID 16560)
-- Name: usuario id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.usuario ALTER COLUMN id SET DEFAULT nextval('public.usuario_id_seq'::regclass);


--
-- TOC entry 2055 (class 2604 OID 16578)
-- Name: votacao id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.votacao ALTER COLUMN id SET DEFAULT nextval('public.votacao_id_seq'::regclass);


--
-- TOC entry 2188 (class 0 OID 16566)
-- Dependencies: 199
-- Data for Name: recurso; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.recurso (id, nome, descricao, habilitado) FROM stdin;
\.


--
-- TOC entry 2186 (class 0 OID 16557)
-- Dependencies: 197
-- Data for Name: usuario; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.usuario (id, nome, email, senha, isadmin) FROM stdin;
\.


--
-- TOC entry 2190 (class 0 OID 16575)
-- Dependencies: 201
-- Data for Name: votacao; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.votacao (id, idusuario, idrecurso, datahora, domentario) FROM stdin;
\.


--
-- TOC entry 2200 (class 0 OID 0)
-- Dependencies: 198
-- Name: recurso_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.recurso_id_seq', 1, false);


--
-- TOC entry 2201 (class 0 OID 0)
-- Dependencies: 196
-- Name: usuario_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.usuario_id_seq', 1, false);


--
-- TOC entry 2202 (class 0 OID 0)
-- Dependencies: 200
-- Name: votacao_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.votacao_id_seq', 1, false);


--
-- TOC entry 2059 (class 2606 OID 16572)
-- Name: recurso recurso_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.recurso
    ADD CONSTRAINT recurso_pkey PRIMARY KEY (id);


--
-- TOC entry 2057 (class 2606 OID 16563)
-- Name: usuario usuario_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.usuario
    ADD CONSTRAINT usuario_pkey PRIMARY KEY (id);


--
-- TOC entry 2061 (class 2606 OID 16580)
-- Name: votacao votacao_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.votacao
    ADD CONSTRAINT votacao_pkey PRIMARY KEY (id);


--
-- TOC entry 2063 (class 2606 OID 16586)
-- Name: votacao votacao_idrecurso_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.votacao
    ADD CONSTRAINT votacao_idrecurso_fkey FOREIGN KEY (idrecurso) REFERENCES public.recurso(id);


--
-- TOC entry 2062 (class 2606 OID 16581)
-- Name: votacao votacao_idusuario_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.votacao
    ADD CONSTRAINT votacao_idusuario_fkey FOREIGN KEY (idusuario) REFERENCES public.usuario(id);


-- Completed on 2018-12-12 21:15:12

--
-- PostgreSQL database dump complete
--

