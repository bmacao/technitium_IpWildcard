/*
This program is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.
You should have received a copy of the GNU General Public License
along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/

using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using DnsServerCore.ApplicationCommon;
using TechnitiumLibrary.Net.Dns;
using TechnitiumLibrary.Net.Dns.ResourceRecords;

namespace technitium_ip_wildcard;

public class App : IDnsApplication, IDnsAppRecordRequestHandler
{

    public void Dispose()   
        {
            //do nothing
        }

    public Task InitializeAsync(IDnsServer dnsServer, string config)
        {
            //do nothing
            return Task.CompletedTask;
        }
        
        //Get the question domain and start processing
        //Ignores TLD e domain name, processes the rest
        //Accepts all notations refered at https://nip.io/ - except IP with hexadecimal notation
        static public string retrieveArecord(String record)
        {
            var aux= record.Split('.','-');
            int reduce = aux.Length -2; 
            Array.Resize<string>(ref aux, reduce);
            string Arec="";
            int j = 0;
            int y= 0;
            List<int> i = new List<int>();
            foreach (var octet in aux)
            {
                //extract octets
                if(int.TryParse(octet, out _) == true){
                    Arec+=octet+'.';
                    i.Add(j);
                };
                j ++;
            }

            //check if valid notation by position
            //127.0.0.1 OK - from 127.0.0.1.domain.tld
            //NOT OK -> 127.x.0.0.1.domain.tld
             for(int z = 0; z < 3; z++)
            {
                int a=i[z+1];
                int b=i[z]+1;

                if(a==b){
                Console.WriteLine("Element#{0} = {1}", z, i[z]);
                y++;
                }
            }

            //remove trailing . from process above -set up valid IP notation
            if(y==3){
            string Arecord = Arec.TrimEnd('.');  
            if (Arecord.Length < 4){
                return null;
            }

            System.Net.IPAddress ipAddress = null;
            bool isValidIp = System.Net.IPAddress.TryParse(Arecord, out ipAddress);

            if(isValidIp){
                return Arecord;
            }else{
                return null;
            }
            }else{
                return null;
            }

        }

    public Task<DnsDatagram> ProcessRequestAsync(DnsDatagram request, IPEndPoint remoteEP, DnsTransportProtocol protocol, bool isRecursionAllowed, string zoneName, uint appRecordTtl, string appRecordData)
        {
            DnsResourceRecord answer;
            //DnsQuestionRecord question = request.Question[0];

            switch (request.Question[0].Type)
            {
                 case DnsResourceRecordType.A:
                  
                    string req = request.Question[0].Name;
                    string addr = retrieveArecord(req);

                    System.Net.IPAddress ipAddress = null;
                    System.Net.IPAddress address = null;
                    bool isValidIp = System.Net.IPAddress.TryParse(addr, out ipAddress);

                    if(isValidIp){
                        address = IPAddress.Parse(addr);
                        }else{
                            return Task.FromResult<DnsDatagram>(null);
                        }
                    

                   answer = new DnsResourceRecord(request.Question[0].Name, DnsResourceRecordType.A, DnsClass.IN, appRecordTtl, new DnsARecordData(address));
          
                    break; 
                  

            default:
                    return Task.FromResult<DnsDatagram>(null);
            }

            


            return Task.FromResult(new DnsDatagram(request.Identifier, true, request.OPCODE, true, false, request.RecursionDesired, isRecursionAllowed, false, false, DnsResponseCode.NoError, request.Question, new DnsResourceRecord[] { answer }));
            //return Task.FromResult(new DnsDatagram(request.Identifier, true, request.OPCODE, true, false, request.RecursionDesired, isRecursionAllowed, false, false, DnsResponseCode.NoError, request.Question, new DnsResourceRecord[] { answer }));

        }
        
        public string Description
        { get { return "Returns the IP address from requests. Similar to https://nip.io/ - except IP with hexadecimal notation"; } }

        public string ApplicationRecordDataTemplate
        { get { return null; } } 

        
}

