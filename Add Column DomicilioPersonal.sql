1. ACTUALIZACION 

alter table DomiciliosPersonal 
drop column CambiaClave;

alter table DomiciliosPersonal 
drop column Clave;

alter table DomiciliosPersonal 
add  Clave varchar(100);

update DomiciliosPersonal set Clave = 'e10adc3949ba59abbe56e057f20f883e';

alter table DomiciliosPersonal 
add Chofer BIT CONSTRAINT [DF_DomiciliosPersonal_Chofer] DEFAULT ((0)) NULL, 
CambiaClave BIT CONSTRAINT [DF_DomiciliosPersonal_CambiaClave] DEFAULT ((0)) NULL,
Clave varchar(100);

alter table DomiciliosPersonal 
add 
Telefono varchar(20),
Correo varchar(50),
EstadoActulizacion varchar(50),
FechaSolicitudActualizacion datetime,
FechaAprobacionRechazoSolicitud datetime,
DatosActualizacion varchar(4000);



CREATE TABLE [dbo].[Checkin] (
    [Idcheckin]       BIGINT        IDENTITY (1, 1) NOT NULL,
    [IdUsuario]       BIGINT        not NULL,
    [IdRecorrido]     BIGINT        not NULL,
    [FechaHora]      DATETIME      NULL,
	[Latitud]                         VARCHAR (50)   NULL,
    [Longitud]                        VARCHAR (50)   NULL
	
);

ALTER TABLE [dbo].[Checkin]
    ADD CONSTRAINT [PK_Checkin] PRIMARY KEY CLUSTERED ([Idcheckin] ASC) WITH (STATISTICS_NORECOMPUTE = ON);



-- 3. Agrego columnas para manejar el cambio de recorrido

alter table DomiciliosPersonal
add CambioRuta BIT NULL;
	

select * from DomiciliosPersonal;