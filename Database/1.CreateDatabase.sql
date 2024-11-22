CREATE SCHEMA company AUTHORIZATION postgres;

CREATE TABLE company.company (
	id int4 GENERATED ALWAYS AS IDENTITY( INCREMENT BY 1 MINVALUE 1 MAXVALUE 2147483647 START 1 CACHE 1 NO CYCLE) NOT NULL,
	"name" varchar NOT NULL,
	exchange varchar NOT NULL,
	ticker varchar NOT NULL,
	isin varchar NOT NULL,
	website varchar NULL,
	CONSTRAINT company_pk PRIMARY KEY (id),
	CONSTRAINT newtable_unique UNIQUE (isin)
);