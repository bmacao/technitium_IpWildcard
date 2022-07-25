# technitium_IpWildcard
Returns the IP address from requests. Similar to https://nip.io/ - except IP with hexadecimal notation

### Version 0.9 ( unrestricted )
##### This version will response to any query that match the rules

Install:
Download .zip and install as app at technitium DNS server

Usage:
Make a query to server using any notation as presented at https://nip.io/ ( except hexadecimal notation )

Example:

dig x-192.0-0-1.bla.bla @[technitium DNS server IP]

; <<>> DiG 9.11.3-1ubuntu1.16-Ubuntu <<>> x-192.0-0-1.bla.bla @[technitium DNS server IP]
;; global options: +cmd
;; Got answer:
;; ->>HEADER<<- opcode: QUERY, status: NOERROR, id: 55779
;; flags: qr aa rd ra; QUERY: 1, ANSWER: 1, AUTHORITY: 0, ADDITIONAL: 1

;; OPT PSEUDOSECTION:
; EDNS: version: 0, flags:; udp: 1232
;; QUESTION SECTION:
;x-192.0-0-1.bla.bla.           IN      A

;; ANSWER SECTION:
x-192.0-0-1.bla.bla.    80      IN      A       192.0.0.1

;; Query time: 12 msec
;; SERVER: 192.168.1.234#53[technitium DNS server IP]
;; WHEN: Thu Jul 21 10:07:15 WEST 2022
;; MSG SIZE  rcvd: 64



### Version 0.9.5
This version will response **only** to existent zones at server

Install:
Download .zip and install as app at technitium DNS server & add APP record to zone:
- Name: @
- App Name: Select dropdown
- Class Path: Select dropdown

Usage:
Make a query to server using any notation as presented at https://nip.io/ ( except hexadecimal notation )

Example:

dig x-192.0-0-1.bla.bla @[technitium DNS server IP]

; <<>> DiG 9.11.3-1ubuntu1.16-Ubuntu <<>> x-192.0-0-1.bla.bla @[technitium DNS server IP]
;; global options: +cmd
;; Got answer:
;; ->>HEADER<<- opcode: QUERY, status: NOERROR, id: 55779
;; flags: qr aa rd ra; QUERY: 1, ANSWER: 1, AUTHORITY: 0, ADDITIONAL: 1

;; OPT PSEUDOSECTION:
; EDNS: version: 0, flags:; udp: 1232
;; QUESTION SECTION:
;x-192.0-0-1.bla.bla.           IN      A

;; ANSWER SECTION:
x-192.0-0-1.bla.bla.    80      IN      A       192.0.0.1

;; Query time: 12 msec
;; SERVER: 192.168.1.234#53[technitium DNS server IP]
;; WHEN: Thu Jul 21 10:07:15 WEST 2022
;; MSG SIZE  rcvd: 64



### Version 0.9.5.1
This version will response **only** to existent zones at server & **dashed** sub-domains

Install:
Download .zip and install as app at technitium DNS server & add APP record to zone:
- Name: @
- App Name: Select dropdown
- Class Path: Select dropdown

Usage:
Make a query to server using dashed notation:
Valid:
- x-192-0-0-1.bla.bla 
- 192-0-0-1.bla.bla

Invalid ( empty response):
- x.192-0-0-1.bla.bla
- 192.0.0.1.bla.bla


Example:

dig x-192-0-0-1.bla.bla @[technitium DNS server IP]

; <<>> DiG 9.11.3-1ubuntu1.16-Ubuntu <<>> x-192-0-0-1.bla.bla @[technitium DNS server IP]
;; global options: +cmd
;; Got answer:
;; ->>HEADER<<- opcode: QUERY, status: NOERROR, id: 55779
;; flags: qr aa rd ra; QUERY: 1, ANSWER: 1, AUTHORITY: 0, ADDITIONAL: 1

;; OPT PSEUDOSECTION:
; EDNS: version: 0, flags:; udp: 1232
;; QUESTION SECTION:
;x-192-0-0-1.bla.bla.           IN      A

;; ANSWER SECTION:
x-192-0-0-1.bla.bla.    80      IN      A       192.0.0.1

;; Query time: 12 msec
;; SERVER: 192.168.1.234#53[technitium DNS server IP]
;; WHEN: Thu Jul 21 10:07:15 WEST 2022
;; MSG SIZE  rcvd: 64