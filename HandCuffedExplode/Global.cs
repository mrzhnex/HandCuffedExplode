using System;

namespace HandCuffedExplode
{
    public static class Global
    {
        public static float distance;
        public static bool plugin_enable;
        public static float saveTime = 0f;


        //rework study
        public static float distance_to_handcuff = 2.0f;

        public static string _notlook = "Вы не смотрите на того, на кого хотите повесить браслет (или находитесь слишком близко)";
        public static string _alreadyhandcuffed = "Указанный человек уже имеет браслет";
        public static string _successhandcuff = "Вы повесили браслет на ";


        public static string _successremove = "Вы сняли браслет с ";
        public static string _notlookremove = "Вы не смотрите на того, с кого хотите снять браслет";
        public static string _nothandcuffed = "Указанный человек не связан";

        public static string _havenothandcuff = "Возьмите наручники в руки";

        //rework 2
        public static string _isscp = "Нельзя привязать этого игрока, так как он - SCP";
        public static Random rand = new Random();
        public static float cooldown_to_ate = 60.0f;
        public static float cooldown_to_uncuff = 30.0f;
        public static string _iscooldown = "Подождите еще: ";
        public static string _badtryuncuff = "Вы попытались развязать, но ничего не получилось";
        public static float distance_to_stay_cuff = 0.5f;

        public static float time_to_ate = 5.0f;
        public static string _already_try_ate = "Вы уже пытаетесь развязаться";
        public static string _startate = "Вы пытаетесь развязаться...";

        public static string _trycufftarget = "Вы чувствуете, что вас пытаются привязать к поверхности/объекту. Не двигайтесь в течение 5 секунд";

        public static string _targetismoving = "Связать не удалось: цель сдвинулась";
        public static string _startcuffproccess = "Вы начали связывать ";

        public static string _toolong = "Связывание остановилось: вы слишком далеко";
        internal static bool can_use_commands;

        public static string _adminUsage = "Вас связали высшие силы. Даже не пытайтесь";
    }
}