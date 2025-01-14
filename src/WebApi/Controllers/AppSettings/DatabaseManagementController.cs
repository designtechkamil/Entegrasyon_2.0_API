using Application.Features.DatabaseManagement.Commands.Create;
using Application.Features.DatabaseManagement.Queries.GetList;
using Application.Features.DatabaseManagement.Queries.TableControls;
using Application.Features.Users.Commands.Create;
using Application.Interfaces.DatabaseManagementModule;
using Application.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.AppSettings
{
	[Route("api/[controller]")]
	[ApiController]
	public class DatabaseManagementController : BaseController
	{
		private readonly IDatabaseManagementService _databaseManagementService;

		public DatabaseManagementController(IDatabaseManagementService databaseManagementService)
		{
			_databaseManagementService = databaseManagementService;
		}

		[HttpPost]
		public async Task<IActionResult> Add()
		{
			var result = await _databaseManagementService.SetupTablels();

			try
			{
				// CreateUserCommand işlemi, hata yönetimi ile birlikte
				CreateUserCommand createUserCommand = new CreateUserCommand
				{
					Email = "admin@designtech.com.tr",
					FullName = "admin",
					Password = "admin",
					Role = Domain.Enums.Role.SuperAdmin

				};

				CreatedUserResponse createdUserResponse = await Mediator.Send(createUserCommand);
			}
			catch (Exception)
			{
			}


				return Created(uri: "", result);
			
		}

		[HttpGet]
		public async Task<IActionResult> GetList()
		{
			GetListDatabaseQuery getListDatabaseQuery = new();
			GetListResponse<GetListDatabaseListItemDto> response = await Mediator.Send(getListDatabaseQuery);
			return Ok(response);
		}


		[HttpGet("TableControls")]
		public async Task<IActionResult> TableControls()
		{
			/*
			 * NOOT:
		 Burada biz sistemimize ait olan tabloların sql de kurulu olup olmadığının kontrolünü yaptıracaz ki yatrıyoruz başlangıç olarak ama buraya şunuda eklememiz lazım biz yüklü olan tablonun IsActive değerini true döndürüyoruz ama yüklenmesi gereken tabloların tutulduğu dosya için de IsActive müdahale etmiyoruz yani sadece geriye dönen değerin değişiyor fiziksel bir değişklik değil bunu fiziksel yapmamız lazım yani dosya üzerinde de false ise true ya çevirecek zaten kurulumu yapılmışsa.

			Bunu hallelttikten sonra arayüz üzzezriden Login sayfası açıldığında otomatik arkada istek ataak bunu her sayfa yenilendiğinde yapacak eğer yüklenmeyen tablo var ise giriş engellenecek yani giriş yapamayacak orada kurulumu tamamla butonu çıkacak ona basınca ya yantarafata açılır pencere yapıp tabloları gösterirriz yada direkt yüklemeyi başlat deyip tabloalrır kurmaya başlatırız bu kurulum sürecini de ekranda loading popup ile falan birşey yaparız belki bu sonraki iş.
		 */
			TableControlsDatabaseQuery tableControlsDatabaseQuery = new();
			List<TableControlsDatabaseListItemDto> response = await Mediator.Send(tableControlsDatabaseQuery);
			return Ok(response);
		}
	}
}
