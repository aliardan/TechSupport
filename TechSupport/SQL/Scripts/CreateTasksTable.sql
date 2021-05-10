-- Table: public.tasks

-- DROP TABLE public.tasks;

CREATE TABLE public.tasks
(
    id integer NOT NULL GENERATED ALWAYS AS IDENTITY ( INCREMENT 1 START 10 MINVALUE 1 MAXVALUE 2147483647 CACHE 1 ),
    id integer NOT NULL GENERATED ALWAYS AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 2147483647 CACHE 1 ),
    name character varying(120) COLLATE pg_catalog."default" NOT NULL,
    description character varying(2000) COLLATE pg_catalog."default" NOT NULL,
    creatorid integer NOT NULL,
    executorid integer,
    status integer NOT NULL,
    priority integer NOT NULL,
    createddatetime timestamp without time zone NOT NULL,
    closeddatetime timestamp without time zone NOT NULL,
    CONSTRAINT tasks_pkey PRIMARY KEY (id),
    CONSTRAINT tasks_creatorid_fkey FOREIGN KEY (creatorid)
        REFERENCES public.users (id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION,
    CONSTRAINT tasks_executorid_fkey FOREIGN KEY (executorid)
        REFERENCES public.users (id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
)

TABLESPACE pg_default;

ALTER TABLE public.tasks
    OWNER to postgres;