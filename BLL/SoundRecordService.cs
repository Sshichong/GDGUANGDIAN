using Model.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    interface SoundRecordService
    {
        List<SoundRecordDto> getSoundRecordAll(string databaseAddress, string databaseName, string anotherName, string userName, string databasePwd);

        List<SoundRecordDto> getSoundRecordByKey(string databaseAddress, string databaseName, string userName, string anotherName, string databasePwd, string starttime, string endtime, string key);
    }
}
