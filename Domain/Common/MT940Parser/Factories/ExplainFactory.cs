using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Common.MT940Parser.Types;

namespace Domain.Common.MT940Parser.Factories
{
    public static class ExplainFactory
    {
        public static string Create(string TransactionType, CreditDebitIdentificationType CreditDebitIdentification)
        {
            string result = "";
            switch (TransactionType)
            {
                case "BND": result = "Bono "; break;
                case "BOE": result = CreditDebitIdentification == CreditDebitIdentificationType.Credit ? "Senet tahsilatı " : "Senet tahsilatı (ters işlem)"; break;
                case "CHG": result = CreditDebitIdentification == CreditDebitIdentificationType.Credit ? "Banka masrafları (ters işlem) " : "Banka masrafları "; break;
                case "CHK": result = CreditDebitIdentification == CreditDebitIdentificationType.Credit ? "Çek tahsilatı " : "Çek ödemesi "; break;
                case "COM": result = CreditDebitIdentification == CreditDebitIdentificationType.Credit ? "Teminat mektubu komisyonu (ters işlem) " : "Teminat mektubu komisyonu "; break;
                case "DDB": result = CreditDebitIdentification == CreditDebitIdentificationType.Credit ? "Doğrudan borçlandırma (Müşteri hesabından otomatik tahsilat) " : "Doğrudan borçlandırma (Müşteri hesabından otomatik tahsilat) (ters işlem) "; break;
                case "EFT": result = CreditDebitIdentification == CreditDebitIdentificationType.Credit ? "Gelen EFT " : "Çıkan EFT "; break;
                case "EXP": result = CreditDebitIdentification == CreditDebitIdentificationType.Credit ? "ihracat tahsilatı " : "ihracat tahsilatı (ters işlem ) "; break;
                case "FEX": result = CreditDebitIdentification == CreditDebitIdentificationType.Credit ? "Döviz satışı " : "Döviz alışı "; break;
                case "FND": result = CreditDebitIdentification == CreditDebitIdentificationType.Credit ? "Stopaj üzerinden fon kesintisi (ters işlem) " : "Stopaj üzerinden fon kesintisi "; break;
                case "IFN": result = CreditDebitIdentification == CreditDebitIdentificationType.Credit ? "Yatırım Fonu Satış " : "Yatırım Fonu Alış "; break;
                case "IMC": result = CreditDebitIdentification == CreditDebitIdentificationType.Credit ? "İthalat Masrafı (ters işlem) " : "İthalat Masrafı "; break;
                case "IMP": result = CreditDebitIdentification == CreditDebitIdentificationType.Credit ? "İthalat Ödemesi  (Ters İşlem) " : "İthalat Ödemesi "; break;
                case "INS": result = CreditDebitIdentification == CreditDebitIdentificationType.Credit ? "Repo Geliri " : "Repo Geliri (Ters işlem) "; break;
                case "INT": result = CreditDebitIdentification == CreditDebitIdentificationType.Credit ? "Faiz Geliri (Vadeli hesap faiz geliri ve yıl sonu faiz geliri) " : "Kredi faiz ödemesi "; break;
                case "LDP": result = CreditDebitIdentification == CreditDebitIdentificationType.Credit ? "Kredi açılışı " : "Kredi kapanışı (Ana para) "; break;
                case "MSC": result = "Diğer İşlemler "; break;
                case "PRO": result = CreditDebitIdentification == CreditDebitIdentificationType.Credit ? "Karşılıksız senet protesto masrafı (ters işlem) " : "Karşılıksız senet protesto masrafı "; break;
                case "SEC": result = CreditDebitIdentification == CreditDebitIdentificationType.Credit ? "Repo geri dönüşü (Ana para) " : "Repo açılışı "; break;
                case "STM": result = CreditDebitIdentification == CreditDebitIdentificationType.Credit ? "Damga Vergisi (ters işlem) " : "Damga Vergisi  "; break;
                case "SUF": result = CreditDebitIdentification == CreditDebitIdentificationType.Credit ? "KKDF ödemesi  (ters işlem) " : "KKDF ödemesi "; break;
                case "TAX": result = CreditDebitIdentification == CreditDebitIdentificationType.Credit ? "Stopaj kesintisi (Ters işlem) " : "Stopaj kesintisi "; break;

                case "TDP": result = CreditDebitIdentification == CreditDebitIdentificationType.Credit ? "Vadeli Hesap Kapanışı ( Ana para) " : "Vadeli Hesap Açılışı "; break;
                case "TRF": result = CreditDebitIdentification == CreditDebitIdentificationType.Credit ? "Gelen Havale " : "Satıcı hesabına gönderilen havale ve EFT LER "; break;
                case "VRM": result = CreditDebitIdentification == CreditDebitIdentificationType.Credit ? "Virman " : "Virman (ters işlem)"; break;
                case "CCP": result = CreditDebitIdentification == CreditDebitIdentificationType.Credit ? "Kredi Kartı Tahsilatı " : "Kredi Kartı Ödemesi"; break;
                case "COC": result = CreditDebitIdentification == CreditDebitIdentificationType.Credit ? "Kredi Kartı Tahsilatı Komisyon" : "Kredi Kartı Ödemesi Komisyonu"; break;
                case "STX": result = CreditDebitIdentification == CreditDebitIdentificationType.Credit ? "Özel işlem vergisi (ters işlem) " : "Özel işlem vergisi  "; break;
                case "NKT": result = CreditDebitIdentification == CreditDebitIdentificationType.Credit ? "Kasadan Nakit Girişler (Yurt içi)" : "Kasadan Nakit Çıkışlar (Yurt içi)"; break;
                case "MHT": result = CreditDebitIdentification == CreditDebitIdentificationType.Credit ? "0003  Kodlu Vergi Tahakkuku" : "0003  Kodlu Vergi Tahakkuku (ters işlem)"; break;
                case "KDV": result = CreditDebitIdentification == CreditDebitIdentificationType.Credit ? "0015  Kodlu Vergi Tahakkuku" : "0015  Kodlu Vergi Tahakkuku (ters işlem)"; break;
                case "GHT": result = CreditDebitIdentification == CreditDebitIdentificationType.Credit ? "0033  Kodlu Vergi Tahakkuku" : "0033  Kodlu Vergi Tahakkuku (ters işlem)"; break;
                case "KCK": result = CreditDebitIdentification == CreditDebitIdentificationType.Credit ? "Kendi Çekimizin Ödenmesi" : "Kendi Çekimizin Ödenmesi (ters işlem)"; break;
                case "KSN": result = CreditDebitIdentification == CreditDebitIdentificationType.Credit ? "Kendi Senedimizin  Ödenmesi" : "Kendi Senedimizin  Ödenmesi (ters işlem)"; break;
                case "SSK": result = CreditDebitIdentification == CreditDebitIdentificationType.Credit ? "SSK ödemeleri" : "SSK ödemeleri (ters işlem)"; break;
            }

            return result;
        }
    }
}