-- Table: public.roles

-- DROP TABLE public.roles;

CREATE TABLE public.roles
(
    id integer NOT NULL GENERATED ALWAYS AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 2147483647 CACHE 1 ),
    id integer NOT NULL GENERATED ALWAYS AS IDENTITY ( INCREMENT 1 START 10 MINVALUE 1 MAXVALUE 2147483647 CACHE 1 ),
    name character varying(20) COLLATE pg_catalog."default" NOT NULL,
    CONSTRAINT roles_pkey PRIMARY KEY (id)
)

TABLESPACE pg_default;

ALTER TABLE public.roles
    OWNER to postgres;