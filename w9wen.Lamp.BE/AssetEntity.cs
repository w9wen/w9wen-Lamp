namespace w9wen.Lamp.BE
{
    public class AssetEntity
    {
        public int Id { get; set; }

        /// <summary>
        /// 取得或設定資產編號
        /// </summary>
        /// <value>資產編號</value>
        public string AssetNo { get; set; }

        /// <summary>
        /// 取得或設定舊的資產編號
        /// </summary>
        /// <value>舊的資產編號</value>
        public string OldAssetNo { get; set; }

        /// <summary>
        /// 取得或設定資產名稱
        /// </summary>
        /// <value>資產名稱</value>
        public string AssetName { get; set; }

        /// <summary>
        /// 取得或設定保管者
        /// </summary>
        /// <value>保管者</value>
        public string Custodian { get; set; }

        /// <summary>
        /// 取得或設定保管地
        /// </summary>
        /// <value>保管地</value>
        public string CustodyLocation { get; set; }

        /// <summary>
        /// 取得或設定資產所屬部門
        /// </summary>
        /// <value>資產所屬部門</value>
        public string CustodyDepartment { get; set; }
    }
}