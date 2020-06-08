-- select * from CabeceraRutasTransportes;
select * from empresa where razonsocial like '%Yp%'

ALTER TABLE [dbo].[CabeceraRutasTransportes]
    ADD CONSTRAINT [FK_CabeceraRutasTransportes_Empresa] FOREIGN KEY ([DestinoRuta]) REFERENCES [dbo].[Empresa] ([IdEmpresa]);

Select * from [CabeceraRutasTransportes]

