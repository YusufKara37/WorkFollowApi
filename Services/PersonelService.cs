using AutoMapper;

using WorkFvApi.Models;

public class PersonelService : IPersonelService
{
    private IGenericRepository<Personel> _genericRepo;
    private IMapper _mapper;

    public PersonelService(IGenericRepository<Personel> genericRepo, IMapper mapper)
    {
        _genericRepo = genericRepo;
        _mapper = mapper;
    }
    public async Task<PersonelDto> Create(PersonelDto personel)
    {
        var dmoModel = _mapper.Map<Personel>(personel);
        var result = await _genericRepo.CreateAsync(dmoModel);
        return _mapper.Map<PersonelDto>(result);
    }

    public async Task<bool> Delete(int personelId)
    {
        return await _genericRepo.DeleteAsync(personelId);
    }

    public async Task<List<PersonelDto>> GetAllPersonels()
    {
        var dmoList = await _genericRepo.GetAllAsync();
        return _mapper.Map<List<PersonelDto>>(dmoList);
    }

    public async Task<PersonelDto> GetById(int id)
    {
        var dmoModel = await _genericRepo.GetByIdAsync(id);
        return _mapper.Map<PersonelDto>(dmoModel);
    }

    public async Task<bool> Update(PersonelDto personelDto)
    {
        // update islemi servise icinde yapilmali, cunku ilk basta veritabanindan mevuct kullanici getirtilecek, 
        // sonra gelen bu kullanici uzerinde degisiklik yapilip geri gonderilmeli
        // dogrudan controller da yapilirsa biraz risli olabiliyor, cunku controllerda veritabanindan gelen model ile kullanicinin gonderdigi model birbirine karisabilir.
        // yani kisaca durum su;

        // id ile veritabindan cektigimiz personeli tekrar geri gondermemiz geriyor Update fonksiyonunda, baska bir model gonderirsek hata verir, guncelleme gibi algilamaz yeni bir kullanici ekleme islemi gibi algilar.
        // guncelleme oldugundan emin olmak icin isi burda yapacagiz.

        var existingPersonel = await _genericRepo.GetByIdAsync(personelDto.PersonelId); // db'den useri bulmaya calis
        if (existingPersonel == null)
        {
            return false; // bulamazsa false don
        }
        // bulursa ilgili alanlari tek tek kontrol edip, icinde deger olan yerleri sadece degistiyoruz
        if (!string.IsNullOrEmpty(personelDto.PersonelName)) // PersonelName prop'u icinde bir deger gonderilmisse
            existingPersonel.PersonelName = personelDto.PersonelName; // gelen degeri veritabanindan cektigimiz entity icine yerlesitr

        // asagidakiler ayni mantikla ilerliyor
        if (!string.IsNullOrEmpty(personelDto.PersonelUserName))
            existingPersonel.PersonelUserName = personelDto.PersonelUserName;

        if (!string.IsNullOrEmpty(personelDto.PersonelPassword))
            existingPersonel.PersonelPassword = personelDto.PersonelPassword;

        if (personelDto.PersonelUnitId.HasValue)
            existingPersonel.PersonelUnitId = personelDto.PersonelUnitId.Value;

        if (personelDto.PersonelAuthoritesId != 0) // bu prop nullable olmadigi icin hasValue kontrolu yapilmaz, cunku asla null olmayacak, en kotu ihtimalle 0 gelecek ama asla null olmayacak, 0 gelirse dokunmuyorz, 0 disinda bir degerle gelirse ayni sekilde veri tabanindan cekilen modelin icine yerlestirilecek.
            existingPersonel.PersonelAuthoritesId = personelDto.PersonelAuthoritesId;

        return await _genericRepo.UpdateAsync(existingPersonel); // guncelleme islemini yapip true ve ya false donecek

    }
}