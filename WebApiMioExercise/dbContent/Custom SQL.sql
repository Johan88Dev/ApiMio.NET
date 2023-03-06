
/* Denna SQL kod kommer fylla en lokal SQL server databas med data från db.json. 

	5: Kopiera nu hela sökvägen till db.json i dbContent mappen och klistra in på de två rödmarkerade områdena i custom SQL filen (rad 11 och rad 38)
	
	När detta är klart, så kör hela nedanstående SQL kod i ett svep. 
	Då kommer en db skapas lokalt och endpointsen blir tillgängliga */

Declare @json varchar(max)
SELECT @json=BulkColumn	 /* Denna röda sökväg måste bytas ut  - Här plockas alla produkter med Rea-kampanj ut och läggs i en tabell */
FROM OPENROWSET (BULK 'C:\Users\johan\Source\Repos\WebApiMioExercise\WebApiMioExercise\dbContent\db.json', SINGLE_NCLOB, CODEPAGE = '65001') as import

SELECT TableA.id, TableA.name, TableA.description,
CampaignTable.name as Rea, CampaignTable.discountPercent, TableA.productImage, TableA.price into dbo.reaTabell
FROM OPENJSON (@JSON)
WITH
(
	[id] varchar(64),
	[name] varchar(64),
	[description] varchar(1024),
	[campaign] nvarchar(max) as JSON,
	[productImage] varchar(128),
	[price] varchar(6)
) as TableA
CROSS APPLY OPENJSON (TableA.campaign)
WITH (

	name varchar(64),
	discountPercent varchar(10)
) as CampaignTable



/* Importera Rak JSON Struktur: Plocka ut delar baserat på titel och sätt samman i en ny tabell */

Declare @json1 varchar(max)
SELECT @json1=BulkColumn	/*     Denna måste också bytas mot er lokala sökväg till db.json. Denna plockar ur alla produkter som inte är på Rea    */
FROM OPENROWSET (BULK 'C:\Users\johan\Source\Repos\WebApiMioExercise\WebApiMioExercise\dbContent\db.json', SINGLE_NCLOB, CODEPAGE = 'UTF-8') import
SELECT * into dbo.TestTabell
FROM OPENJSON (@JSON1)
WITH
(
	[id] varchar(64),
	[name] varchar(30),
	[description] varchar(1024),
	[campaign] varchar(512),
	[reapris] varchar(32),
	[productImage] varchar(128),
	[price] varchar(6)
)

/* Hantera NULL värden i kolumner förutom Id = PK */

alter table dbo.Products
alter column campaign varchar(24) NULL;

alter table dbo.Products
alter column reapris varchar(24) NULL;

alter table dbo.Products
alter column imageUrl varchar(128) NULL;

alter table dbo.Products
alter column description varchar(1024) NULL;

alter table dbo.Products
alter column name varchar(64) NULL;

alter table dbo.Products
alter column price varchar(24) NULL;

/* Stoppa in i måltabell  => dbo.Products */

insert into dbo.Products
select * from dbo.TestTabell;

/* fixa null värden */

update dbo.Products
set campaign = 'Null'
where campaign IS NULL;

update dbo.Products
set reapris = 'Null'
where reapris IS NULL;

delete from dbo.Products
where imageUrl IS Null;

/* till sist så slås de olika tabellerna samman igen så vi har alla produkter samlande i en tabell som sedan används av APIt */
UPDATE dbo.Products 
SET dbo.Products.campaign = dbo.reaTabell.Rea, 
dbo.Products.reapris = dbo.reaTabell.discountPercent
FROM dbo.reaTabell
WHERE dbo.Products.Id = dbo.reaTabell.id;
