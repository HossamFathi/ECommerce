using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Settings.Helper
{
    public interface ISettingService
    {
       
        Task<SettingDTO> get();
        Task<bool> Update(int SettingID, SettingDTO SettingDTO);
        Task<bool> Delete(int SettingID);
        Task Insert(SettingDTO SettingDTO);
    }
}
