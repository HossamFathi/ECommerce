using AutoMapper;
using DataBaseLayer;
using DataBaseLayer.models;
using DTO;
using DTO.Entities.Setting;
using ServiceLayer.Settings.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Settings
{
    internal class SettingLogic : ISettingService
    {
        private readonly IRepository<Setting> _setting;
        private readonly IMapper _mapper;
        public SettingLogic(IRepository<Setting> setting, IMapper mapper)
        {
            _setting = setting;
            _mapper = mapper;
        }

        public async Task<bool> Delete(int SettingID)
        {
          return await  _setting.Delete(SettingID);
                
        }

        public async Task<SettingDTO> get()
        {
            Setting setting = (await _setting.GetAll()).FirstOrDefault();
            return ConvertToSettingDTO(setting);

        }

        private SettingDTO ConvertToSettingDTO(Setting setting)
        {
           return _mapper.Map<SettingDTO>(setting);
        }
        private Setting ConvertToSetting(SettingDTO setting)
        {
          return  _mapper.Map<Setting>(setting);
        }


      
        public async Task Insert(SettingDTO SettingDTO)
        {
          Setting setting=   ConvertToSetting(SettingDTO);
           await _setting.InsertEntityAsync(setting); 
        }

        public async Task<bool> Update(int SettingID, SettingDTO SettingDTO)
        {
         Setting setting =  await _setting.SingleOrDefaultAsync(set => set.ID == SettingID);
            if (setting == null)
                return false; 
           _mapper.Map(SettingDTO, setting, typeof(SettingDTO) , typeof(Setting));
           return await _setting.update(setting);
        }
    }
}
