alter table DomiciliosPersonal 
add Chofer BIT CONSTRAINT [DF_DomiciliosPersonal_Chofer] DEFAULT ((0)) NULL, 
CambiaClave BIT CONSTRAINT [DF_DomiciliosPersonal_CambiaClave] DEFAULT ((0)) NULL,
Clave varchar(10);

select * from DomiciliosPersonal;