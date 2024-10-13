namespace LibraryRaschet
{
    [Serializable]

    public class TverdTelo
    {
        #region Входные параметры 

        ///Выход пара
        private double _parVyh;
        public double ParVyh
        { get { return _parVyh; }
            set { _parVyh = value; }
        }

        ///Расход воды на продувку котла
        private double _rashWat;
        public double RashWat
        {
            get { return _rashWat; }
            set { _rashWat = value; }
        }

        ///Температура питательной воды
        private double _tempPitWat;
        public double TempPitWat
        {
            get { return _tempPitWat; }
            set { _tempPitWat = value; }
        }


        ///Температура нагретого пара
        private double _tempHeatWat;
        public double TempHeatWat
        {
            get { return _tempHeatWat; }
            set { _tempHeatWat = value; }
        }

        ///Давление
        private double _pressure;
        public double Pressure
        {
            get { return _pressure; }
            set { _pressure = value; }
        }

        ///Температура рабочего топлива
        private double _tempRabT;
        public double TempRabT
        {
            get { return _tempRabT; }
            set { _tempRabT = value; }
        }

        ///Содержание водорода
        private double _sodH;
        public double SodH
        {
            get { return _sodH; }
            set { _sodH = value; }
        }

        ///Содержание углерода
        private double _sodC;
        public double SodC
        {
            get { return _sodC; }
            set { _sodC = value; }
        }

        ///Содержание кислорода
        private double _sodO;
        public double SodO
        {
            get { return _sodO; }
            set { _sodO = value; }
        }


        ///Содержание серы
        private double _sodS;
        public double SodS
        {
            get { return _sodS; }
            set { _sodS = value; }
        }


        ///Содержание водяных паров
        private double _sodWP;
        public double SodWP
        {
            get { return _sodWP; }
            set { _sodWP = value; }
        }

        ///Избыток воздуха
        private double _alpha;
        public double Alpha
        {
            get { return _alpha; }
            set { _alpha = value; }
        }
        ///Температура воздуха
        private double _tempVozd;
        public double TempVozd
        {
            get { return _tempVozd; }
            set { _tempVozd = value; }
        }

        ///Температура уходящих газов
        private double _tempOut;
        public double TempOut
        {
            get { return _tempOut; }
            set { _tempOut = value; }
        }
        ///Заряд химической неполноты сгорания
        private double _qChem;
        public double QChem
        {
            get { return _qChem; }
            set { _qChem = value; }
        }
        ///Заряд механической неполноты сгорания
        private double _qMech;
        public double QMech
        {
            get { return _qMech; }
            set { _qMech = value; }
        }
        ///Заряд наружного охлаждения
        private double _qCold;
        public double QCold
        {
            get { return _qCold; }
            set { _qCold = value; }
        }
        ///Заряд тепла с физическим теплом шлаков
        private double _qWarm;
        public double QWarm
        {
            get { return _qWarm; }
            set { _qWarm = value; }
        }

        #endregion Входные параметры 


        #region Расчетные данные
        #region ТЕПЛОТА,ЗАТРАЧЕННАЯ НА РАЗЛОЖЕНИЕ КАРБОНАТОВ Q_k
        /// <summary>
        /// Энтальпия питательной воды
        /// </summary>
        private double _entPitWat;

        public double EntPitWat()
        {
            double _entWat = 2 * Math.Pow(10, -7) * _tempPitWat + Math.Pow(10, -4) * _tempPitWat + 1.495;//энтальпия воды
            _entPitWat= _tempPitWat*_entWat;//энтальпия воды*темп.пит.воды
            return _entPitWat;
        }
        /// <summary>
        /// Энтальпия нагретого пара
        /// </summary>
        private double _entHeatWat;
        public double EntHeatWat()
        {
            double _entPrWat = 2 * Math.Pow(10, -7) * _tempHeatWat + Math.Pow(10, -4) * _tempHeatWat + 1.495;//энтальпия прегретой воды
            _entHeatWat = _tempHeatWat * _entPrWat;
            return _entHeatWat;
        }

        /// <summary>
        /// Энтальпия кипящей воды
        /// </summary>
        private double _entBoilWat;
        public double EntBoilWat()
        {
            double _tempBoil = -1.5606 * Math.Pow(_pressure, 2)+22.167*_pressure+80.335;//температура кипящей воды
            double _entBoil = 2 * Math.Pow(10, -7) * _tempBoil + Math.Pow(10, -4) * _tempBoil + 1.495;//энтальпия кипятка
            _entBoilWat = _tempBoil * _entBoil;
            return _entBoilWat;
        }

        // <summary>
        /// ТЕПЛОТА,ЗАТРАЧЕННАЯ НА РАЗЛОЖЕНИЕ КАРБОНАТОВ Q_k
        /// </summary>
        private double _warmQk;
        public double WarmQk()
        {
            _warmQk = _parVyh * (_entHeatWat - _entPitWat) + _parVyh * _rashWat*(_entBoilWat - _entPitWat);
            return _warmQk;
        }

        #endregion ТЕПЛОТА,ЗАТРАЧЕННАЯ НА РАЗЛОЖЕНИЕ КАРБОНАТОВ Q_k

        #region РАСЧЕТ РАСПОЛАГАЕМОГО ТЕПЛА Q_p
        /// <summary>
        /// Физическое тепло топлива
        /// </summary>
        private double _warmFuel;
        public double WarmFuel()
        {
        
            double _teploemk = 0.9575+0.0013* _tempRabT+7* Math.Pow(10, -7)* Math.Pow(_tempRabT, 2);//теплоемкость рабочего топлива
            _warmFuel = _teploemk * _tempRabT;
            return _warmFuel;
        }


        /// <summary>
        /// низшая теплотворная способность (теплота сгорания) топлива
        /// </summary>
        private double _warmBurnLow;
        public double WarmBurn()
        {

            _warmBurnLow = 339*_sodC+1030*_sodH-109*(_sodO+_sodS)-25.14*(9*_sodH+_sodWP);
            return _warmBurnLow;
        }

        /// <summary>
        /// удельная теплота парообразования
        /// </summary>
        private double _lambdaAlpha;
        public double LambdaAlpha()
        {

            _lambdaAlpha = 7.6456*_alpha-0.0948;
            return _lambdaAlpha;
        }
        /// <summary>
        /// высшая теплотворная способность (теплота сгорания) топлива
        /// </summary>
        private double _warmBurnHigh;
        public double WarmBurnHigh()
        {

            _warmBurnHigh = (_lambdaAlpha* _tempVozd*(_n2*79+_o2*21))/100;
            return _warmBurnHigh;
        }

        /// <summary>
        /// теплоемкость газов  азота
        /// </summary>
        private double _n2;
        public double N2()
        {
            _n2 = Math.Pow(10,-7)* Math.Pow(_tempOut, 2)+4* Math.Pow(10, -6)* _tempOut+1.2956;
            return _n2;
        }
        /// <summary>
        /// теплоемкость газов  кислорода
        /// </summary>
        private double _o2;
        public double O2()
        {
            _o2 = 2*Math.Pow(10, -7) * Math.Pow(_tempOut, 2) +  Math.Pow(10, -4) * _tempOut + 1.3064;
            return _o2;
        }
        /// <summary>
        /// теплоемкость газов  углекислого газа co2
        /// </summary>
        private double _co2;
        public double CO2()
        {
            _co2 =- 6 * Math.Pow(10, -7) * Math.Pow(_tempOut, 2) + Math.Pow(10, -3) * Math.Pow(_tempOut, 2) + 1.6015;
            return _co2;
        }

        /// <summary>
        /// теплоемкость газов  сернистого газа so2
        /// </summary>
        private double _so2;
        public double SO2()
        {
            _so2 = -3 * Math.Pow(10, -7) * Math.Pow(_tempOut, 2) +8* Math.Pow(10, -4) * _tempOut + 1.733;
            return _so2;
        }

        /// <summary>
        /// теплоемкость газов  воды
        /// </summary>
        private double _h2o;
        public double H2O()
        {
            _h2o = 2 * Math.Pow(10, -7) * Math.Pow(_tempOut, 2) +  Math.Pow(10, -4) * _tempOut + 1.495;
            return _h2o;
        }

        /// <summary>
        /// РАСПОЛАГАЕМОЕ ТЕПЛО Q_p
        /// </summary>
        private double _warmHave;
        public double WarmHave()
        {
            _warmHave = _warmBurnHigh+ _warmBurnLow+ _warmFuel;
            return _warmHave;
        }
        #endregion РАСЧЕТ РАСПОЛАГАЕМОГО ТЕПЛА Q_p

        #region РАСЧЕТ КПД 

        /// <summary>
        /// средний состав продуктов сгорания углекислого газа co2
        /// </summary>
        private double _burnco2;
        public double BurnCO2()
        {
            _burnco2 = 6.3739*Math.Pow(_alpha, 2) -27.008*_alpha+37.11;
            return _burnco2;
        }

        /// <summary>
        /// средний состав продуктов сгорания сернистого газа so2
        /// </summary>
        private double _burnso2;
        public double BurnSO2()
        {
            _burnso2 = 0.0501 * Math.Pow(_alpha, 2) - 0.1797 * _alpha + 0.189;
            return _burnso2;
        }

        /// <summary>
        /// средний состав продуктов сгорания воды h2o
        /// </summary>
        private double _burnh2o;
        public double BurnH2O()
        {
            _burnh2o = 1.5821 * Math.Pow(_alpha, 2) - 9.5428 * _alpha + 19.178;
            return _burnh2o;
        }

        /// <summary>
        /// средний состав продуктов сгорания азота n2
        /// </summary>
        private double _burnn2;
        public double BurnN2()
        {
            _burnn2 = -1.1926 * Math.Pow(_alpha, 2) + 6.464 * _alpha + 66.372;
            return _burnn2;
        }

        /// <summary>
        /// средний состав продуктов сгорания кислоорода о2
        /// </summary>
        private double _burno2;
        public double BurnO2()
        {
            _burno2 = -8.0356 * Math.Pow(_alpha, 2) + 34 * _alpha + 25.958;
            return _burno2;
        }


        /// <summary>
        /// необходимый объем воздуха
        /// </summary>
        private double _valpha;
        public double VAlpha()
        {
            _valpha = 7.6*_alpha+0.67;
            return _valpha;
        }

        /// <summary>
        /// энтальпия уходящих газов
        /// </summary>
        private double _entOutGas;
        public double EntOutGas()
        {
            double sum = (_co2 * _burnco2 + _so2 * _burnso2 + _h2o * _burnh2o) + _burnn2 * _n2 + _burno2 * _o2;
            _entOutGas = (_tempOut*sum)/100;
            return _entOutGas;
        }
        // <summary>
        ///потеря с уходящими газами
        /// </summary>
        private double _lossGas;
        public double LossGas()
        {
            _lossGas = _entOutGas* _valpha;
            return _lossGas;
        }
        // <summary>
        ///потеря от химической неполноты сгорания
        /// </summary>
        private double _lossChem;
        public double LossChem()
        {
            _lossChem = (_qChem* _warmHave)/100;
            return _lossChem;
        }

        // <summary>
        ///потеря от механической неполноты сгорания
        /// </summary>
        private double _lossMech;
        public double LossMech()
        {
            _lossMech =(_qMech * _warmHave) / 100;
            return _lossChem;
        }

        // <summary>
        ///потеря от наружного охлаждения
        /// </summary>
        private double _lossOutCold;
        public double LossOutCold()
        {
            _lossOutCold = (_qCold * _warmHave) / 100;
            return _lossOutCold;
        }

        // <summary>
        ///потеря от тепла с физ.шлаком.нигде не используется
        /// </summary>
        private double _lossOutWarm;
        public double LossOutWarm()
        {
            _lossOutWarm = (_qWarm * _warmHave) / 100;
            return _lossOutWarm;
        }

        // <summary>
        ///Расходная часть теплового баланса
        /// </summary>
        private double _lossFullWarm;
        public double LossFullWarm()
        {
            _lossFullWarm = _lossGas+ _lossChem+ _lossMech+ _lossOutCold+ _warmQk;
            return _lossFullWarm;
        }

        // <summary>
        ///КПД
        /// </summary>
        private double _kpd;
        public double KPD()
        {
            double _lossZar = (_lossFullWarm*100)/ _warmHave;
            _kpd = 100 - _lossZar;
            return _kpd;
        }
        #endregion РАСЧЕТ КПД

        #region ЧАСОВОЙ РАСХОД ТОПЛИВА ИТОГ
        // <summary>
        ///Часовой расход топлива
        /// </summary>
        private double _rashodTopl;
        public double RashodTopl()
        {
            
            _rashodTopl = (_warmQk*100)/(_kpd* _warmHave);
            return _rashodTopl;
        }
        #endregion ЧАСОВОЙ РАСХОД ТОПЛИВА ИТОГ
        #endregion Расчетные данные
    }


}