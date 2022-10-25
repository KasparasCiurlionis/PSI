using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace WebProject
{
    public class GasStations : IEnumerable
    {
        String name;
        private GasStation[] _gasStation;
        public GasStations( GasStation[] pArray, string name)
        {
            _gasStation = new GasStation[pArray.Length];

            for (int i = 0; i < pArray.Length; i++)
            {
                _gasStation[i] = pArray[i];
            }

            this.name = name;
        }

        public String getName()
        {
            return name;
        }

        public void setName(String name)
        {
            this.name = name;
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)GetEnumerator();
        }

        public GasEnumerator GetEnumerator()
        {
            return new GasEnumerator(_gasStation);
        }
    }

    public class GasEnumerator : IEnumerator
    {
        private GasStation[] _gasStation;
        int position = -1;

        public GasEnumerator(GasStation[] list)
        {
            _gasStation = list;
        }

        public bool MoveNext()
        {
            position++;
            return (position < _gasStation.Length);
        }

        public void Reset()
        {
            position = -1;
        }

        object IEnumerator.Current
        {
            get
            {
                return Current;
            }
        }

        public GasStation Current
        {
            get
            {
                try
                {
                    return _gasStation[position];
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }
    }
}