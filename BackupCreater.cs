﻿using System;
using System.IO;
using System.Windows.Forms;

namespace 日志书写器
{
    class BackupCreater
    {
        #region 保存的属性
        /// <summary>
        /// 备份的源文件名。（赋值会同步更新后缀名和备份文件名；可以使用绝对路径或者相对路径，如果使用绝对路径会更新工作路径）
        /// </summary>
        public string Original文件名
        {
            get { return original文件名; }
            set
            {
                if (original文件名 == value) //没有修改就不费事了
                    return;
                original文件名 = value;
                /* 不是初始化过程的时候，要用新的Original文件名更新Backup文件名 */
                if (Backup文件名 != null)
                {
                    /* 删除现在的缓存，不然变更后会变成野缓存 */
                    if (File.Exists(Backup文件名) && original文件名.Contains("\\")) //有缓存且切换目录
                        this.DeleteBackup();
                    /* 同步信息到Backup文件名 */
                    this.Backup后缀名 = this.Backup后缀名;
                    /* 提取工作路径 */
                    int directoryEndIndex = original文件名.LastIndexOf('\\');
                    if (directoryEndIndex != -1)
                        this.workingDirectory = original文件名.Substring(0, directoryEndIndex);
                }
            }
        }
        /// <summary>
        /// 自动保存Timer.Interval（启动后不可以修改）
        /// </summary>
        public int Interval { get { return BackupFileTimer.Interval; } set { if (this.ParametersReadOnly) throw new System.FieldAccessException("开始后不可以修改此变量！"); BackupFileTimer.Interval = value; } }
        /// <summary>
        /// 备份文件的后缀名（如果文件名含多个拓展名，会取得多个拓展名而不是最后一个；赋值会同步更新备份文件名）
        /// </summary>
        public string Backup后缀名
        {
            get
            {
                if (Backup文件名 == null)
                    throw new Exception("Backup文件名未被初始化！");
                // 找到文件名的StartIndex，并提取出来
                string fileName = Backup文件名.Substring(Backup文件名.LastIndexOf("\\") + 1);
                int dotIndex = fileName.IndexOf('.'); // 找到第一个.
                if (dotIndex == -1) return "";
                else
                    return fileName.Substring(dotIndex); // 返回.开始的字符串
            }
            set
            {
                if (value[0] != '.')
                    value = '.' + value;
                int dotIndex = this.original文件名.IndexOf('.');
                if (dotIndex == -1) //源文件没后缀，直接加
                    this.Backup文件名 = this.original文件名 + value;
                else
                    this.Backup文件名 = this.original文件名.Substring(0, dotIndex) + value;
            }
        }
        /// <summary>
        /// BackupCreater的工作路径
        /// </summary>
        public string WorkingDirectory
        {
            get
            {
                return workingDirectory;
            }
            set
            {
                if (workingDirectory == value) //没改工作路径就不用费事了
                    return;
                /* 删除现在的缓存，不然变更后会变成野缓存 */
                if (File.Exists(Backup文件名)) //有缓存且切换目录
                    this.DeleteBackup();
                workingDirectory = value;
                string shortOriginalFileName = Original文件名.Substring(Original文件名.LastIndexOf('\\') + 1);
                if (workingDirectory.EndsWith("\\")) //统一去掉最后的\
                    workingDirectory = workingDirectory.Substring(0, workingDirectory.Length - 1);
                Original文件名 = workingDirectory + "\\" + shortOriginalFileName;
            }
        }
        /// <summary>
        /// 备份文件名
        /// </summary>
        public string Backup文件名 { get; set; }
        /// <summary>
        /// 备份加密算法
        /// </summary>
        public 加密算法 Encrypt算法 { get { return encrypt算法; } set { if (this.ParametersReadOnly) throw new System.FieldAccessException("开始后不可以修改此变量！"); encrypt算法 = value; } }
        /// <summary>
        /// 隐藏备份文件
        /// </summary>
        public bool HiddenBackupFile { get; set; }
        /* 注意：下方变量只能在本region内使用！ */
        private string original文件名;
        private string workingDirectory = Directory.GetCurrentDirectory();
        private 加密算法 encrypt算法;
        #endregion

        public enum 加密算法 { 无 };

        #region 事件注册
        public event WriteProcedure WriteBackupEvent;
        public delegate void WriteProcedure(string writeFileName);
        public delegate void RestoreProcedure();
        #endregion

        private Timer BackupFileTimer { get; set; }
        private bool ParametersReadOnly { get; set; } = false;
        public bool IsBusy { get { return this.BackupFileTimer.Enabled; } }

        /// <summary>
        /// 生成一个新的BackupCreater
        /// </summary>
        /// <param name="备份源文件名">备份源文件名</param>
        /// <param name="writeFileProcedure">写文件函数（要求唯一的参数为写文件名）</param>
        /// <param name="interval">备份间隔(毫秒)</param>
        /// <param name="备份后缀名">备份后缀名</param>
        /// <param name="hideBackup">是否设置备份为隐藏文件</param>
        /// <param name="算法">备份算法</param>
        public BackupCreater(string 备份源文件名, WriteProcedure writeFileProcedure = null, int interval = 1000, string 备份后缀名 = ".backup", bool hideBackup = false, 加密算法 算法 = 加密算法.无)
        {
            this.Original文件名 = 备份源文件名; // 设置源文件名
            if (writeFileProcedure != null) // 注册备份函数
                this.WriteBackupEvent += writeFileProcedure;
            else
                this.WriteBackupEvent += DefaultBackupFunction;
            if (备份后缀名.StartsWith(".") == false) // 后缀名加.
                this.Backup后缀名 = "." + 备份后缀名;
            else
                this.Backup后缀名 = 备份后缀名;
            this.Encrypt算法 = 算法; // 设置加密算法
            this.HiddenBackupFile = hideBackup; // 设置隐藏备份
            if (备份源文件名.Contains(":"))
            {
                // 如果含有:号说明是绝对路径，由于在Original文件名第二次设置开始才会同步workingDirectory。
                // 在这里调用是为了同步workingDirectory
                int directoryEndIndex = original文件名.LastIndexOf('\\');
                if (directoryEndIndex != -1)
                    this.workingDirectory = original文件名.Substring(0, directoryEndIndex);
            }
            /* 内部操作设置 */
            BackupFileTimer = new Timer();
            this.Interval = interval;
            BackupFileTimer.Tick += WriteBackupInvoke;
        }

        /// <summary>
        /// Invoke写备份事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WriteBackupInvoke(object sender, EventArgs e)
        {
            WriteBackupEvent.Invoke(this.Backup文件名);
            if (this.HiddenBackupFile)
                File.SetAttributes(this.Backup文件名, FileAttributes.Hidden);
        }

        /// <summary>
        /// 默认的备份函数。只是复制文件
        /// </summary>
        /// <param name="backupFileName"></param>
        private void DefaultBackupFunction(string backupFileName)
        {
            if (backupFileName != this.Backup文件名)
                throw new Exception("Fetal problem: 参数传递异常！");
            File.Delete(this.Backup文件名);
            File.Copy(this.Original文件名, this.Backup文件名);
        }

        /// <summary>
        /// 默认的还原函数
        /// </summary>
        private void DefaultRestoreFunction()
        {
            File.Delete(this.Original文件名);
            File.Copy(this.Backup文件名, this.Original文件名);
        }

        /// <summary>
        /// 启动自动备份Timer
        /// </summary>
        public void Start()
        {
            if (WriteBackupEvent.GetInvocationList().Length == 0)
                throw new System.Exception("BackupCreater未经初始化就强迫开始执行！");
            this.BackupFileTimer.Start();
            this.ParametersReadOnly = true;
        }

        /// <summary>
        /// 开启自动备份，并返回正在执行的BackupCreater
        /// </summary>
        /// <param name="备份源文件名">备份源文件名</param>
        /// <param name="writeFileProcedure">写文件函数（要求唯一的参数为写文件名）</param>
        /// <param name="interval">备份间隔(毫秒)</param>
        /// <param name="备份后缀名">备份后缀名</param>
        /// <param name="hideBackup">是否设置备份为隐藏文件</param>
        public static BackupCreater Backup(string 备份源文件名, WriteProcedure writeFileProcedure = null, int interval = 1000, string 备份后缀名 = ".backup", bool hideBackup = false)
        {
            var ret = new BackupCreater(备份后缀名, writeFileProcedure, interval, 备份后缀名, hideBackup);
            ret.Start();
            return ret;
        }

        /// <summary>
        /// 停止自动备份Timer
        /// </summary>
        public void Stop()
        {
            this.BackupFileTimer.Stop();
            this.ParametersReadOnly = false;
        }

        /// <summary>
        /// 不使用Timer，单次执行备份操作
        /// </summary>
        public void StartOnce()
        {
            this.WriteBackupInvoke(null, null);
        }
        /// <summary>
        /// 只执行单步备份操作，并返回正在执行的BackupCreater
        /// </summary>
        /// <param name="备份源文件名">备份源文件名</param>
        /// <param name="writeFileProcedure">写文件函数（要求唯一的参数为写文件名）</param>
        /// <param name="备份后缀名">备份后缀名</param>
        /// <param name="hideBackup">是否设置备份为隐藏文件</param>
        public static BackupCreater BackupOnce(string 备份源文件名, WriteProcedure writeFileProcedure = null, string 备份后缀名 = ".backup", bool hideBackup = false)
        {
            var ret = new BackupCreater(备份后缀名, writeFileProcedure, int.MaxValue, 备份后缀名, hideBackup);
            ret.StartOnce();
            return ret;
        }

        /// <summary>
        /// 删除备份文件
        /// </summary>
        public bool DeleteBackup()
        {
            try
            {
                File.Delete(this.Backup文件名);
                return true;
            }
            catch(IOException)
            {
                return false;
            }
        }

        /// <summary>
        /// 使用指定函数从备份文件恢复（不传参则调用默认恢复函数）
        /// </summary>
        /// <param name="restoreProcedure"></param>
        /// <param name="deleteBackupFile"></param>
        public void RestoreFile(RestoreProcedure restoreProcedure = null, bool deleteBackupFile = false)
        {
            if (restoreProcedure == null)
                DefaultRestoreFunction();
            else
                restoreProcedure();
            if (deleteBackupFile)
                this.DeleteBackup();
        }
    }
}
