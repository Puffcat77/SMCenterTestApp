using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helpers
{
    public class LogConstants
    {
        public const string DOCTOR_ID_IS_NULL = "Не указан идентификатор доктора.";
        public const string DOCTOR_ID_NOT_IN_DB = "В базе отсутствует доктор с таким идентификатором {0}.";
        public const string LIST_PARAMETERS_NOT_SET = "Не заданы параметры списка.";
        public const string ORDER_PROPERTY_NOT_RECOGNIZED = "Неизвестный параметр сортировки.";
        public const string DOCTOR_CREATE_EXCEPTION = "Модель данных о докторе некорректна.";
        public const string DOCTOR_WAS_DELETED_WHILE_EDIT = "Доктор был удален из бд другим пользователем.";
        public const string DOCTOR_SUCCESSFULLY_DELETED = "Доктор успешно удален.";
        public const string PATIENT_ID_IS_NULL = "Не указан идентификатор пациента.";
        public const string PATIENT_ID_NOT_IN_DB = "В базе отсутствует пациент с таким идентификатором {0}.";
        public const string PATIENT_CREATE_EXCEPTION = "Модель данных о пациенте некорректна.";
        public const string PATIENT_WAS_DELETED_WHILE_EDIT = "Пациент был удален из бд другим пользователем.";
        public const string PATIENT_SUCCESSFULLY_DELETED = "Пациент успешно удален.";
    }
}
