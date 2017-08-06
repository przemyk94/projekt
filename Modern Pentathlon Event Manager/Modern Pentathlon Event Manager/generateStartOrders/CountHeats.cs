using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modern_Pentathlon_Event_Manager.generateStartOrders
{
    class CountHeats
    {
        private int _number_of_heats;
        private int _participants;
        private int _number_of_lanes;
        private int _modulo_from_heats;
        private int[] _series_table;

        #region Klasa heats() - sluzy do obliczenia liczby zawodnikow w danej serii. 
        //Obliczenie liczby serii, w przypadku uzyskania reszty z dzielenia dodanie dodatkowej serii
        //Liczba zawodnikow w danej serii zapisywana jest do tablicy
        //Pierwszy element w tablicy[0] odpowiada pierwszej serii - najslabszej
        public int[] heats()
        {
            _number_of_heats = _participants / _number_of_lanes;
            _modulo_from_heats = _participants % _number_of_lanes;
            if (_modulo_from_heats != 0)
            {
                _number_of_heats++;
            }
            _series_table = new int[_number_of_heats];

            for (int i = 0; i < _number_of_heats; i++)
            {
                if (_number_of_heats == 1)
                {
                    _series_table[i] = _participants;
                }
                else
                {
                    if (_modulo_from_heats == 0)
                    {
                        _series_table[i] = _number_of_lanes;
                        _participants -= _number_of_lanes;
                    }
                    else
                    {
                        if (_modulo_from_heats == 1)
                        {
                            _series_table[i] = 3;
                            _modulo_from_heats = (_participants - 3) % _number_of_lanes;
                            _participants -= 3;
                        }
                        else
                        {
                            if (_modulo_from_heats == 2)
                            {
                                _series_table[i] = 4;
                                _modulo_from_heats = (_participants - 4) % _number_of_lanes;
                                _participants -= 4;
                            }
                            else
                            {
                                if (_modulo_from_heats >= 3)
                                {
                                    _series_table[i] = _modulo_from_heats;
                                    _modulo_from_heats -= _modulo_from_heats;
                                    _participants -= _modulo_from_heats;
                                }
                            }
                        }
                    }
                }
            }
            return _series_table;
        }
        #endregion


        public CountHeats(int participants, int number_of_lanes)
        {
            _participants = participants;
            _number_of_lanes = number_of_lanes;
        }
    }
}
