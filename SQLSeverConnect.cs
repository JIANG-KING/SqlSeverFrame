﻿using java.lang;
using System.Data;
using System.Data.SqlClient;
namespace SqlSeverFrame
{
    class SQLSeverConnect
    {
        private static readonly string constr = "Server=yun2333.top;Database=Chattools;user id=jiangyun;pwd=Jy1019878449";
        SqlConnection sqlCnt = new SqlConnection(constr);
        public SqlDataAdapter SqlLogin(string username, string password)//登录验证
        {
            sqlCnt.Open();
            string strSQL = "select account,password from LoginInfo where account=@id and password=@pwd";
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = strSQL;
            cmd.Parameters.AddWithValue("@id", username);
            cmd.Parameters.AddWithValue("@pwd", password);
            cmd.Connection = sqlCnt;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            sqlCnt.Close();
            return da;
        }
        public int SqlSearch(string Account)//查询指定用户
        {
            sqlCnt.Open();
            string strSQL = "select account from LoginInfo where account=@id";
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = strSQL;
            cmd.Parameters.AddWithValue("@id", Account);
            cmd.Connection = sqlCnt;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            int a = da.Fill(ds, "LoginInfo");
            sqlCnt.Close();
            return a;
        }
        public int sqlInsert(string Account, string Password, string image, string NickName)//插入一个用户，用户注册时使用
        {
            sqlCnt.Open();

            string strSQL = "INSERT INTO [dbo].[LoginInfo] ([Account],[Password],[ImageHead],[NickName]) VALUES (@id,@pwd,@image,@NickName)";
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = strSQL;
            cmd.Parameters.AddWithValue("@id", Account);
            cmd.Parameters.AddWithValue("@pwd", Password);
            cmd.Parameters.AddWithValue("@image", image);
            cmd.Parameters.AddWithValue("@NickName", NickName);
            cmd.Connection = sqlCnt;
            int result = cmd.ExecuteNonQuery();
            sqlCnt.Close();
            return result;
        }
        public string SearchImage(string username)//查找用户的头像
        {
            sqlCnt.Open();
            string strSQL = "select ImageHead from LoginInfo where account=@id";
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = strSQL;
            cmd.Parameters.AddWithValue("@id", username);
            cmd.Connection = sqlCnt;
            SqlDataReader dr;//创建DataReader对象
            dr = cmd.ExecuteReader();
            string s = "";
            if (dr.Read())
                s = dr["imagehead"].ToString().Substring(0, 5);
            sqlCnt.Close();
            return s;
        }
        public int UpdateState(string username, string LoginState)//更新用户的状态，在线，隐身，忙碌，请勿打扰，q我吧
        {
            sqlCnt.Open();
            string strSQL = "update LoginInfo set AccountState=@LoginState  where Account=@id";
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = strSQL;
            cmd.Parameters.AddWithValue("@id", username);
            cmd.Parameters.AddWithValue("@LoginState", LoginState);
            cmd.Connection = sqlCnt;
            int result = cmd.ExecuteNonQuery();
            sqlCnt.Close();
            return result;
        }
        public string SearchUserState(string username)//查找用户的在线状态
        {
            sqlCnt.Open();
            string strSQL = "select AccountState from LoginInfo where Account=@id";
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = strSQL;
            cmd.Parameters.AddWithValue("@id", username);
            cmd.Connection = sqlCnt;
            SqlDataReader dr;//创建DataReader对象
            dr = cmd.ExecuteReader();
            string s = "";
            if (dr.Read())
                s = dr["AccountState"].ToString();
            sqlCnt.Close();
            return s;
        }
        public string SearchNickname(string username)//查找用户的昵称
        {
            sqlCnt.Open();
            string strSQL = "select NickName from LoginInfo where Account=@id";
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = strSQL;
            cmd.Parameters.AddWithValue("@id", username);
            cmd.Connection = sqlCnt;
            SqlDataReader dr;//创建DataReader对象
            dr = cmd.ExecuteReader();
            string s = "";
            if (dr.Read())
                s = dr["NickName"].ToString();
            sqlCnt.Close();
            return s;
        }
        public int UpdateIsAlive(string username, string IsAlive)//修改用户的登录状态
        {
            sqlCnt.Open();
            string strSQL = "update LoginInfo set IsAlive=@IsAlive where Account=@id";
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = strSQL;
            cmd.Parameters.AddWithValue("@id", username);
            cmd.Parameters.AddWithValue("@IsAlive", IsAlive);
            cmd.Connection = sqlCnt;
            int result = cmd.ExecuteNonQuery();
            sqlCnt.Close();
            return result;
        }
        public int SearchIsAlive(string username)//查找用户是否已登录
        {
            sqlCnt.Open();
            string strSQL = "select IsAlive from LoginInfo where Account=@id";
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = strSQL;
            cmd.Parameters.AddWithValue("@id", username);
            cmd.Connection = sqlCnt;
            SqlDataReader dr;//创建DataReader对象
            dr = cmd.ExecuteReader();
            string s = "";
            if (dr.Read())
                s = dr["IsALive"].ToString();
            sqlCnt.Close();
            return Integer.parseInt(s);
        }
        public string[] SearchFriends(string username)//查找用户的好友
        {
            sqlCnt.Open();
            string strSQL = "select Friends from Friends where UserName=@id";
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = strSQL;
            cmd.Parameters.AddWithValue("@id", username);
            cmd.Connection = sqlCnt;
            SqlDataReader dr;//创建DataReader对象
            dr = cmd.ExecuteReader();
            string[] User = new string[2000];
            int i = 0;
            while (dr.Read())
            {
                User[i] = dr["Friends"].ToString();
                i++;
            }
            sqlCnt.Close();
            return User;
        }
        public int  IsFriends(string username,string friends)//查找是否已经是用户好友
        {
            sqlCnt.Open();
            string strSQL = "select Friends from Friends where UserName=@id and friends=@friends";
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = strSQL;
            cmd.Parameters.AddWithValue("@id", username);
            cmd.Parameters.AddWithValue("@friends", friends);
            cmd.Connection = sqlCnt;
            int result = cmd.ExecuteNonQuery();
            sqlCnt.Close();
            return result;
        }
        public string[] SearchMessage(string username, string Receiver)//查找用户收到的消息，最近的十条
        {
            sqlCnt.Open();
            string strSQL = "SELECT top(10)  [Message] FROM [Chattools].[dbo].[MeassageBox] where Sender=@id And Receiver=@Receiver ORDER by SerialNumber deSC ";
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = strSQL;
            cmd.Parameters.AddWithValue("@id", username);
            cmd.Parameters.AddWithValue("@Receiver", Receiver);
            cmd.Connection = sqlCnt;
            SqlDataReader dr;//创建DataReader对象
            dr = cmd.ExecuteReader();
            string[] User = new string[10];
            int i = 0;
            while (dr.Read())
            {
                User[i] = dr["Message"].ToString();
                i++;
            }
            sqlCnt.Close();
            return User;
        }
        public int SendMessage(string Sender, string Message, string Receiver)//将发送的消息上传到数据库
        {
            sqlCnt.Open();
            string strSQL = "INSERT INTO [dbo].[MeassageBox] ([Sender],[Message],[Receiver]) VALUES (@Sender,@Message ,@Receiver)";
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = strSQL;
            cmd.Parameters.AddWithValue("Sender", Sender);
            cmd.Parameters.AddWithValue("@Receiver", Receiver);
            cmd.Parameters.AddWithValue("Message", Message);
            cmd.Connection = sqlCnt;
           int result = cmd.ExecuteNonQuery();
            sqlCnt.Close();
            return result;
        }
        public int[] SearchNumbers(string username, string Receiver)//查找用户收到或发送的消息的序号
        {
            sqlCnt.Open();
            string strSQL = "SELECT top(10)  [SerialNumber] FROM [Chattools].[dbo].[MeassageBox] where Sender=@username And Receiver=@Receiver ORDER by SerialNumber deSC ";
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = strSQL;
            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@Receiver", Receiver);
            cmd.Connection = sqlCnt;
            SqlDataReader dr;//创建DataReader对象
            dr = cmd.ExecuteReader();
            int[] User = new int[10];
            int i = 0;
            while (dr.Read())
            {
                User[i] = Integer.parseInt(dr["SerialNumber"].ToString());
                i++;
            }
            sqlCnt.Close();
            return User;
        }
        public string SearchMessageByNumber(string SerialNumber)//按序号查询消息
        {
            sqlCnt.Open();
            string strSQL = "select Message  from MeassageBox where SerialNumber=@SerialNumber ";
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = strSQL;
            cmd.Parameters.AddWithValue("@SerialNumber", SerialNumber);
            cmd.Connection = sqlCnt;
            SqlDataReader dr;//创建DataReader对象
            dr = cmd.ExecuteReader();
            string s="";
            if (dr.Read())
            {
                s = dr["Message"].ToString();
            }
            sqlCnt.Close();
            return s;
        }
        public string SearchSenDerByNumber(string SerialNumber)//按序号查询消息发送人
        {
            sqlCnt.Open();
            string strSQL = "select Sender  from MeassageBox where SerialNumber=@SerialNumber ";
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = strSQL;
            cmd.Parameters.AddWithValue("SerialNumber", SerialNumber);
            cmd.Connection = sqlCnt;
            SqlDataReader dr;//创建DataReader对象
            dr = cmd.ExecuteReader();
            string s="";
            if (dr.Read())
            {
                s = dr["Sender"].ToString();
            }
            sqlCnt.Close();
            return s;
        }
        public int  AddFriends(string username,string Friends)//添加好友
        {
            sqlCnt.Open();
            string strSQL = "insert into Friends (Username,friends) values(@Username,@Friends)";
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = strSQL;
            cmd.Parameters.AddWithValue("@Username", username);
            cmd.Parameters.AddWithValue("@Friends", Friends);
            cmd.Connection = sqlCnt;
            int result = cmd.ExecuteNonQuery();
            sqlCnt.Close();
            return result;
        }
        public int SendFriendApplication(string sender,string message, string Friends)//发送好友申请
        {
            sqlCnt.Open();
            string strSQL = "insert into FriendsApplication (sender,message,receiver) values(@sender,@message,@receiver)";
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = strSQL;
            cmd.Parameters.AddWithValue("sender", sender);
            cmd.Parameters.AddWithValue("message", message);
            cmd.Parameters.AddWithValue("receiver", Friends);
            cmd.Connection = sqlCnt;
            int result = cmd.ExecuteNonQuery();
            sqlCnt.Close();
            return result;
        }
        public string[] SearchFriendsApplication(string username)//查找用户的好友申请
        {
            sqlCnt.Open();
            string strSQL = "select sender from FriendsApplication where receiver=@id";
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = strSQL;
            cmd.Parameters.AddWithValue("@id", username);
            cmd.Connection = sqlCnt;
            SqlDataReader dr;//创建DataReader对象
            dr = cmd.ExecuteReader();
            string[] User = new string[2000];
            int i = 0;
            while (dr.Read())
            {
                if (i >= 2000) break;
                User[i] = dr["sender"].ToString();
                i++;
            }
            sqlCnt.Close();
            return User;
        }
        public string SearchFriendsApplicationMessage(string sender,string receiver)//查找用户的好友申请信息
        {
            sqlCnt.Open();
            string strSQL = "select message from FriendsApplication where sender=@sender and receiver=@receiver";
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = strSQL;
            cmd.Parameters.AddWithValue("@sender", sender);
            cmd.Parameters.AddWithValue("@receiver", receiver);
            cmd.Connection = sqlCnt;
            SqlDataReader dr;//创建DataReader对象
            dr = cmd.ExecuteReader();
            string User="";
            if (dr.Read()) {User = dr["message"].ToString(); }           
            sqlCnt.Close();
            return User;
        }

        public int DeleteApplication(string sender,string receiver)//删除好友申请
        {
            sqlCnt.Open();
            string strSQL = "delete from FriendsApplication where sender = @id and receiver=@receiver";
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = strSQL;
            cmd.Parameters.AddWithValue("@id", sender);
            cmd.Parameters.AddWithValue("@receiver", receiver);
            cmd.Connection = sqlCnt;
            int result = cmd.ExecuteNonQuery();
            sqlCnt.Close();
            return result;
        }
        public int  IsSendApplication(string sender,string receiver)//查找是否已经存在好友申请
        {
            sqlCnt.Open();
            string strSQL = "select sender from FriendsApplication where sender=@id and receiver=@receiver";
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = strSQL;
            cmd.Parameters.AddWithValue("@id", sender);
            cmd.Parameters.AddWithValue("@receiver", receiver);
            cmd.Connection = sqlCnt;
            DataSet dataSet = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            int result=da.Fill(dataSet, "sender");
            sqlCnt.Close();
            return result;
        }

    }
    }
