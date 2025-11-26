SQL> CREATE TABLE Employe(
  2  EMPNO number(4),
  3  EMPNOM varchar2(15),
  4  JOB varchar2(15),
  5  SUPERVISEUR number(4),
  6  DATEEMBAUCHE date,
  7  SALAIRE number(8,2),
  8  COMMISSION number(6,2),
  9  DEPNO char(3));

Table created.

SQL> ALTER TABLE Employe add(EMPNOM varchar2(50));
ALTER TABLE Employe add(EMPNOM varchar2(50))
                        *
ERROR at line 1:
ORA-01430: column being added already exists in table 


SQL> ALTER TABLE Employe add(CODEPOSTAL varchar2(50));

Table altered.

SQL> ALTER TABLE Employe modify(EMPNOM varchar2(50));

Table altered.

SQL> ALTER TABLE Employe RENAME COLUMN DATEEMBAUCHE TO EMBAUCHE;

Table altered.

SQL> DESC Employe;
 Name                                      Null?    Type
 ----------------------------------------- -------- ----------------------------
 EMPNO                                              NUMBER(4)
 EMPNOM                                             VARCHAR2(50)
 JOB                                                VARCHAR2(15)
 SUPERVISEUR                                        NUMBER(4)
 EMBAUCHE                                           DATE
 SALAIRE                                            NUMBER(8,2)
 COMMISSION                                         NUMBER(6,2)
 DEPNO                                              CHAR(3)
 CODEPOSTAL                                         VARCHAR2(50)

SQL> ALTER TABLE Employe DROP COLUMN COMMISSION;

Table altered.

SQL> ALTER TABLE Employe RENAME TO Employes;

Table altered.

SQL> ALTER TABLE Employes add(EMPPRENOM varchar2(50), VILLE varchar(30));

Table altered.

SQL> DESC Employes;
 Name                                      Null?    Type
 ----------------------------------------- -------- ----------------------------
 EMPNO                                              NUMBER(4)
 EMPNOM                                             VARCHAR2(50)
 JOB                                                VARCHAR2(15)
 SUPERVISEUR                                        NUMBER(4)
 EMBAUCHE                                           DATE
 SALAIRE                                            NUMBER(8,2)
 DEPNO                                              CHAR(3)
 CODEPOSTAL                                         VARCHAR2(50)
 EMPPRENOM                                          VARCHAR2(50)
 VILLE                                              VARCHAR2(30)

SQL> INSERT INTO Employes VALUES(
  2  7893, King, President, NULL, to_date('21 janvier 2021','dd-mmmm-yyyy'), 286574.55, OPE, G6SJ8R, James, Levis);
7893, King, President, NULL, to_date('21 janvier 2021','dd-mmmm-yyyy'), 286574.55, OPE, G6SJ8R, James, Levis)
                                                                                                       *
ERROR at line 2:
ORA-00984: column not allowed here 


SQL> ALTER SESSION SET NLS_DATE_FORMAT = 'DD month YYYY';

Session altered.

SQL> ALTER SESSION SET NLS_DATE_LANGUAGE = 'french';

Session altered.

SQL> SELECT SYSDATE FROM DUAL;

SYSDATE                                                                         
-----------------                                                               
06 fevrier   2024                                                               

SQL> INSERT INTO Employes VALUES(
  2  '7893', 'King', 'President', NULL, '21 janvier 2021', '286574.55', 'OPE', 'G6SJ8R', 'James', 'Levis'),
  3  ('7566', 'Jones', 'Manager', '7893', '25 janvier 2019', '123456.00', 'CPT', 'G6D9K9', 'Bob', 'Quebec'),
  4  ('7902', 'Ford', 'Analyste', '7566', '24 mars 2021', '75259.85', 'CPT', 'G6D8F9', 'Tom', 'Levis');
'7893', 'King', 'President', NULL, '21 janvier 2021', '286574.55', 'OPE', 'G6SJ8R', 'James', 'Levis'),
                                                                                                     *
ERROR at line 2:
ORA-00933: SQL command not properly ended 


SQL> INSERT INTO Employes VALUES(
  2    2  '7893', 'King', 'President', NULL, '21 janvier 2021', '286574.55', 'OPE', 'G6SJ8R', 'James', 'Levis'),
  3    3  ('7566', 'Jones', 'Manager', '7893', '25 janvier 2019', '123456.00', 'CPT', 'G6D9K9', 'Bob', 'Quebec'),
  4    2  '7893', 'King', 'President', NULL, '21 janvier 2021', '286574.55', 'OPE', 'G6SJ8R', 'James', 'Levis');
  2  '7893', 'King', 'President', NULL, '21 janvier 2021', '286574.55', 'OPE', 'G6SJ8R', 'James', 'Levis'),
     *
ERROR at line 2:
ORA-00917: missing comma 


SQL> INSERT INTO Employes VALUES(
  2    2  '7893', 'King', 'President', NULL, '21 janvier 2021', '286574.55', 'OPE', 'G6SJ8R', 'James', 'Levis'),
  3    3  ('7566', 'Jones', 'Manager', '7893', '25 janvier 2019', '123456.00', 'CPT', 'G6D9K9', 'Bob', 'Quebec'),
  4  
SQL> INSERT INTO Employes VALUES('7893','King','President',NULL,'21 janvier 2021','286574.55','OPE','G6SJ8R','James','Levis');

1 row created.

SQL> INSERT INTO Employes VALUES('7566','Jones','Manager',7893,'25 janvier 2019','123456.00','CPT','G6D9K9','Bob','Quebec');

1 row created.

SQL> INSERT INTO Employes VALUES('7902','Ford','Analyste',7566,'24 mars 2021','75259.85','CPT','G6D8F9','Tom','Levis');

1 row created.

SQL> SELECT * FROM Employes;

     EMPNO EMPNOM                                             JOB               
---------- -------------------------------------------------- ---------------   
SUPERVISEUR EMBAUCHE             SALAIRE DEP                                    
----------- ----------------- ---------- ---                                    
CODEPOSTAL                                                                      
--------------------------------------------------                              
EMPPRENOM                                                                       
--------------------------------------------------                              
VILLE                                                                           
------------------------------                                                  
      7893 King                                               President         
            21 janvier   2021  286574.55 OPE                                    
G6SJ8R                                                                          

     EMPNO EMPNOM                                             JOB               
---------- -------------------------------------------------- ---------------   
SUPERVISEUR EMBAUCHE             SALAIRE DEP                                    
----------- ----------------- ---------- ---                                    
CODEPOSTAL                                                                      
--------------------------------------------------                              
EMPPRENOM                                                                       
--------------------------------------------------                              
VILLE                                                                           
------------------------------                                                  
James                                                                           
Levis                                                                           
                                                                                

     EMPNO EMPNOM                                             JOB               
---------- -------------------------------------------------- ---------------   
SUPERVISEUR EMBAUCHE             SALAIRE DEP                                    
----------- ----------------- ---------- ---                                    
CODEPOSTAL                                                                      
--------------------------------------------------                              
EMPPRENOM                                                                       
--------------------------------------------------                              
VILLE                                                                           
------------------------------                                                  
      7566 Jones                                              Manager           
       7893 25 janvier   2019     123456 CPT                                    
G6D9K9                                                                          

     EMPNO EMPNOM                                             JOB               
---------- -------------------------------------------------- ---------------   
SUPERVISEUR EMBAUCHE             SALAIRE DEP                                    
----------- ----------------- ---------- ---                                    
CODEPOSTAL                                                                      
--------------------------------------------------                              
EMPPRENOM                                                                       
--------------------------------------------------                              
VILLE                                                                           
------------------------------                                                  
Bob                                                                             
Quebec                                                                          
                                                                                

     EMPNO EMPNOM                                             JOB               
---------- -------------------------------------------------- ---------------   
SUPERVISEUR EMBAUCHE             SALAIRE DEP                                    
----------- ----------------- ---------- ---                                    
CODEPOSTAL                                                                      
--------------------------------------------------                              
EMPPRENOM                                                                       
--------------------------------------------------                              
VILLE                                                                           
------------------------------                                                  
      7902 Ford                                               Analyste          
       7566 24 mars      2021   75259.85 CPT                                    
G6D8F9                                                                          

     EMPNO EMPNOM                                             JOB               
---------- -------------------------------------------------- ---------------   
SUPERVISEUR EMBAUCHE             SALAIRE DEP                                    
----------- ----------------- ---------- ---                                    
CODEPOSTAL                                                                      
--------------------------------------------------                              
EMPPRENOM                                                                       
--------------------------------------------------                              
VILLE                                                                           
------------------------------                                                  
Tom                                                                             
Levis                                                                           
                                                                                

SQL> INSERT INTO Employes (EMPNO,EMPNOM,JOB,SALAIRE) VALUES ('7845','Chabot','Programmeur','65699.00');

1 row created.

SQL> INSERT INTO Employes VALUES('7902','Ford','Vendeur',7566,SYSDATE,'75000.00','VTE','G6D6G6','Tom','Levis');

1 row created.

SQL> UPDATE Employes SET EMPNO = 7934 WHERE CODEPOSTAL = 'G6D6G6';

1 row updated.

SQL> UPDATE Employes SET SUPERVISEUR = 7902, DEPNO = 'INF' WHERE EMPNO = '7845';

1 row updated.

SQL> INSERT INTO Employes VALUES ('7369','Smith','Secretaire','7902','3 novembre 2015','OPE',NULL,'Julia',NULL);
INSERT INTO Employes VALUES ('7369','Smith','Secretaire','7902','3 novembre 2015','OPE',NULL,'Julia',NULL)
            *
ERROR at line 1:
ORA-00947: not enough values 


SQL> INSERT INTO Employes (EMPNO,EMPNOM,JOB,DEPNO,CODEPOSTAL,EMPPRENOM,VILLE) VALUES ('7369','Smith','Secretaire','7902','3 novembre 2015','OPE',NULL,'Julia',NULL);
INSERT INTO Employes (EMPNO,EMPNOM,JOB,DEPNO,CODEPOSTAL,EMPPRENOM,VILLE) VALUES ('7369','Smith','Secretaire','7902','3 novembre 2015','OPE',NULL,'Julia',NULL)
            *
ERROR at line 1:
ORA-00913: too many values 


SQL> INSERT INTO Employes (EMPNO,EMPNOM,JOB,SUPERVISEUR,EMBAUCHE,DEPNO,CODEPOSTAL,EMPPRENOM,VILLE) VALUES ('7369','Smith','Secretaire','7902','3 novembre 2015','OPE',NULL,'Julia',NULL);

1 row created.

SQL> INSERT INTO Employes (EMPNO,EMPNOM,JOB,SUPERVISEUR,EMBAUCHE,DEPNO,CODEPOSTAL,EMPPRENOM,VILLE) VALUES ('7698','Blake','Manager','7893',SYSDATE,'INF',NULL,'Tom',NULL);

1 row created.

SQL> SELECT * FROM Employes;

     EMPNO EMPNOM                                             JOB               
---------- -------------------------------------------------- ---------------   
SUPERVISEUR EMBAUCHE             SALAIRE DEP                                    
----------- ----------------- ---------- ---                                    
CODEPOSTAL                                                                      
--------------------------------------------------                              
EMPPRENOM                                                                       
--------------------------------------------------                              
VILLE                                                                           
------------------------------                                                  
      7893 King                                               President         
            21 janvier   2021  286574.55 OPE                                    
G6SJ8R                                                                          

     EMPNO EMPNOM                                             JOB               
---------- -------------------------------------------------- ---------------   
SUPERVISEUR EMBAUCHE             SALAIRE DEP                                    
----------- ----------------- ---------- ---                                    
CODEPOSTAL                                                                      
--------------------------------------------------                              
EMPPRENOM                                                                       
--------------------------------------------------                              
VILLE                                                                           
------------------------------                                                  
James                                                                           
Levis                                                                           
                                                                                

     EMPNO EMPNOM                                             JOB               
---------- -------------------------------------------------- ---------------   
SUPERVISEUR EMBAUCHE             SALAIRE DEP                                    
----------- ----------------- ---------- ---                                    
CODEPOSTAL                                                                      
--------------------------------------------------                              
EMPPRENOM                                                                       
--------------------------------------------------                              
VILLE                                                                           
------------------------------                                                  
      7566 Jones                                              Manager           
       7893 25 janvier   2019     123456 CPT                                    
G6D9K9                                                                          

     EMPNO EMPNOM                                             JOB               
---------- -------------------------------------------------- ---------------   
SUPERVISEUR EMBAUCHE             SALAIRE DEP                                    
----------- ----------------- ---------- ---                                    
CODEPOSTAL                                                                      
--------------------------------------------------                              
EMPPRENOM                                                                       
--------------------------------------------------                              
VILLE                                                                           
------------------------------                                                  
Bob                                                                             
Quebec                                                                          
                                                                                

     EMPNO EMPNOM                                             JOB               
---------- -------------------------------------------------- ---------------   
SUPERVISEUR EMBAUCHE             SALAIRE DEP                                    
----------- ----------------- ---------- ---                                    
CODEPOSTAL                                                                      
--------------------------------------------------                              
EMPPRENOM                                                                       
--------------------------------------------------                              
VILLE                                                                           
------------------------------                                                  
      7902 Ford                                               Analyste          
       7566 24 mars      2021   75259.85 CPT                                    
G6D8F9                                                                          

     EMPNO EMPNOM                                             JOB               
---------- -------------------------------------------------- ---------------   
SUPERVISEUR EMBAUCHE             SALAIRE DEP                                    
----------- ----------------- ---------- ---                                    
CODEPOSTAL                                                                      
--------------------------------------------------                              
EMPPRENOM                                                                       
--------------------------------------------------                              
VILLE                                                                           
------------------------------                                                  
Tom                                                                             
Levis                                                                           
                                                                                

     EMPNO EMPNOM                                             JOB               
---------- -------------------------------------------------- ---------------   
SUPERVISEUR EMBAUCHE             SALAIRE DEP                                    
----------- ----------------- ---------- ---                                    
CODEPOSTAL                                                                      
--------------------------------------------------                              
EMPPRENOM                                                                       
--------------------------------------------------                              
VILLE                                                                           
------------------------------                                                  
      7845 Chabot                                             Programmeur       
       7902                        65699 INF                                    
                                                                                

     EMPNO EMPNOM                                             JOB               
---------- -------------------------------------------------- ---------------   
SUPERVISEUR EMBAUCHE             SALAIRE DEP                                    
----------- ----------------- ---------- ---                                    
CODEPOSTAL                                                                      
--------------------------------------------------                              
EMPPRENOM                                                                       
--------------------------------------------------                              
VILLE                                                                           
------------------------------                                                  
                                                                                
                                                                                
                                                                                

     EMPNO EMPNOM                                             JOB               
---------- -------------------------------------------------- ---------------   
SUPERVISEUR EMBAUCHE             SALAIRE DEP                                    
----------- ----------------- ---------- ---                                    
CODEPOSTAL                                                                      
--------------------------------------------------                              
EMPPRENOM                                                                       
--------------------------------------------------                              
VILLE                                                                           
------------------------------                                                  
      7934 Ford                                               Vendeur           
       7566 06 fevrier   2024      75000 VTE                                    
G6D6G6                                                                          

     EMPNO EMPNOM                                             JOB               
---------- -------------------------------------------------- ---------------   
SUPERVISEUR EMBAUCHE             SALAIRE DEP                                    
----------- ----------------- ---------- ---                                    
CODEPOSTAL                                                                      
--------------------------------------------------                              
EMPPRENOM                                                                       
--------------------------------------------------                              
VILLE                                                                           
------------------------------                                                  
Tom                                                                             
Levis                                                                           
                                                                                

     EMPNO EMPNOM                                             JOB               
---------- -------------------------------------------------- ---------------   
SUPERVISEUR EMBAUCHE             SALAIRE DEP                                    
----------- ----------------- ---------- ---                                    
CODEPOSTAL                                                                      
--------------------------------------------------                              
EMPPRENOM                                                                       
--------------------------------------------------                              
VILLE                                                                           
------------------------------                                                  
      7369 Smith                                              Secretaire        
       7902 03 novembre  2015            OPE                                    
                                                                                

     EMPNO EMPNOM                                             JOB               
---------- -------------------------------------------------- ---------------   
SUPERVISEUR EMBAUCHE             SALAIRE DEP                                    
----------- ----------------- ---------- ---                                    
CODEPOSTAL                                                                      
--------------------------------------------------                              
EMPPRENOM                                                                       
--------------------------------------------------                              
VILLE                                                                           
------------------------------                                                  
Julia                                                                           
                                                                                
                                                                                

     EMPNO EMPNOM                                             JOB               
---------- -------------------------------------------------- ---------------   
SUPERVISEUR EMBAUCHE             SALAIRE DEP                                    
----------- ----------------- ---------- ---                                    
CODEPOSTAL                                                                      
--------------------------------------------------                              
EMPPRENOM                                                                       
--------------------------------------------------                              
VILLE                                                                           
------------------------------                                                  
      7698 Blake                                              Manager           
       7893 06 fevrier   2024            INF                                    
                                                                                

     EMPNO EMPNOM                                             JOB               
---------- -------------------------------------------------- ---------------   
SUPERVISEUR EMBAUCHE             SALAIRE DEP                                    
----------- ----------------- ---------- ---                                    
CODEPOSTAL                                                                      
--------------------------------------------------                              
EMPPRENOM                                                                       
--------------------------------------------------                              
VILLE                                                                           
------------------------------                                                  
Tom                                                                             
                                                                                
                                                                                

7 rows selected.

SQL> SELECT EMPNO, EMPNOM, EMPPRENOM FROM Employes;

     EMPNO EMPNOM                                                               
---------- --------------------------------------------------                   
EMPPRENOM                                                                       
--------------------------------------------------                              
      7893 King                                                                 
James                                                                           
                                                                                
      7566 Jones                                                                
Bob                                                                             
                                                                                
      7902 Ford                                                                 
Tom                                                                             
                                                                                

     EMPNO EMPNOM                                                               
---------- --------------------------------------------------                   
EMPPRENOM                                                                       
--------------------------------------------------                              
      7845 Chabot                                                               
                                                                                
                                                                                
      7934 Ford                                                                 
Tom                                                                             
                                                                                
      7369 Smith                                                                
Julia                                                                           
                                                                                

     EMPNO EMPNOM                                                               
---------- --------------------------------------------------                   
EMPPRENOM                                                                       
--------------------------------------------------                              
      7698 Blake                                                                
Tom                                                                             
                                                                                

7 rows selected.

SQL> SELECT EMPNO, EMPNOM, EMPPRENOM, SUPERVISEUR FROM Employes;

     EMPNO EMPNOM                                                               
---------- --------------------------------------------------                   
EMPPRENOM                                          SUPERVISEUR                  
-------------------------------------------------- -----------                  
      7893 King                                                                 
James                                                                           
                                                                                
      7566 Jones                                                                
Bob                                                       7893                  
                                                                                
      7902 Ford                                                                 
Tom                                                       7566                  
                                                                                

     EMPNO EMPNOM                                                               
---------- --------------------------------------------------                   
EMPPRENOM                                          SUPERVISEUR                  
-------------------------------------------------- -----------                  
      7845 Chabot                                                               
                                                          7902                  
                                                                                
      7934 Ford                                                                 
Tom                                                       7566                  
                                                                                
      7369 Smith                                                                
Julia                                                     7902                  
                                                                                

     EMPNO EMPNOM                                                               
---------- --------------------------------------------------                   
EMPPRENOM                                          SUPERVISEUR                  
-------------------------------------------------- -----------                  
      7698 Blake                                                                
Tom                                                       7893                  
                                                                                

7 rows selected.

SQL> SELECT UPPER(EMPNOM), EMPPRENOM, TO_CHAR(EMBAUCHE,'day DD month') FROM Employes;

UPPER(EMPNOM)                                                                   
--------------------------------------------------                              
EMPPRENOM                                                                       
--------------------------------------------------                              
TO_CHAR(EMBAUCHE,'DAYDDMONTH')                                                  
------------------------------------------------------------------------        
KING                                                                            
James                                                                           
jeudi    21 janvier                                                             
                                                                                
JONES                                                                           
Bob                                                                             
vendredi 25 janvier                                                             

UPPER(EMPNOM)                                                                   
--------------------------------------------------                              
EMPPRENOM                                                                       
--------------------------------------------------                              
TO_CHAR(EMBAUCHE,'DAYDDMONTH')                                                  
------------------------------------------------------------------------        
                                                                                
FORD                                                                            
Tom                                                                             
mercredi 24 mars                                                                
                                                                                
CHABOT                                                                          
                                                                                

UPPER(EMPNOM)                                                                   
--------------------------------------------------                              
EMPPRENOM                                                                       
--------------------------------------------------                              
TO_CHAR(EMBAUCHE,'DAYDDMONTH')                                                  
------------------------------------------------------------------------        
                                                                                
                                                                                
FORD                                                                            
Tom                                                                             
mardi    06 fevrier                                                             
                                                                                
SMITH                                                                           

UPPER(EMPNOM)                                                                   
--------------------------------------------------                              
EMPPRENOM                                                                       
--------------------------------------------------                              
TO_CHAR(EMBAUCHE,'DAYDDMONTH')                                                  
------------------------------------------------------------------------        
Julia                                                                           
mardi    03 novembre                                                            
                                                                                
BLAKE                                                                           
Tom                                                                             
mardi    06 fevrier                                                             
                                                                                

7 rows selected.

SQL> SELECT EMPNO, EMPNOM, EMPPRENOM, MONTHS_BETWEEN(sysdate, EMBAUCHE) AS NbMois FROM Employes;

     EMPNO EMPNOM                                                               
---------- --------------------------------------------------                   
EMPPRENOM                                              NBMOIS                   
-------------------------------------------------- ----------                   
      7893 King                                                                 
James                                              36.5478984                   
                                                                                
      7566 Jones                                                                
Bob                                                60.4188661                   
                                                                                
      7902 Ford                                                                 
Tom                                                34.4511242                   
                                                                                

     EMPNO EMPNOM                                                               
---------- --------------------------------------------------                   
EMPPRENOM                                              NBMOIS                   
-------------------------------------------------- ----------                   
      7845 Chabot                                                               
                                                                                
                                                                                
      7934 Ford                                                                 
Tom                                                         0                   
                                                                                
      7369 Smith                                                                
Julia                                              99.1285435                   
                                                                                

     EMPNO EMPNOM                                                               
---------- --------------------------------------------------                   
EMPPRENOM                                              NBMOIS                   
-------------------------------------------------- ----------                   
      7698 Blake                                                                
Tom                                                         0                   
                                                                                

7 rows selected.

SQL> SELECT COUNT(*) FROM Employes;

  COUNT(*)                                                                      
----------                                                                      
         7                                                                      

SQL> SELECT SUM(SALAIRE) FROM Employes;

SUM(SALAIRE)                                                                    
------------                                                                    
    625989.4                                                                    

SQL> SELECT EMPNOM, EMPPRENOM,
  2  EMPNOM || ' ' || EMPPRENOM ' a ete embauche un ' || TO_CHAR(EMBAUCHE, 'Day');
EMPNOM || ' ' || EMPPRENOM ' a ete embauche un ' || TO_CHAR(EMBAUCHE, 'Day')
                           *
ERROR at line 2:
ORA-00923: FROM keyword not found where expected 


SQL> SELECT EMPNOM, EMPPRENOM,
  2  EMPNOM || ' ' || EMPPRENOM ' a ete embauche un ' || TO_CHAR(EMBAUCHE, 'Day') FROM Employes;
EMPNOM || ' ' || EMPPRENOM ' a ete embauche un ' || TO_CHAR(EMBAUCHE, 'Day') FROM Employes
                           *
ERROR at line 2:
ORA-00923: FROM keyword not found where expected 


SQL> SELECT EMPNOM, EMPPRENOM,
  2  EMPNOM || ' ' || EMPPRENOM || ' a ete embauche un ' || TO_CHAR(EMBAUCHE, 'Day') FROM Employes;

EMPNOM                                                                          
--------------------------------------------------                              
EMPPRENOM                                                                       
--------------------------------------------------                              
EMPNOM||''||EMPPRENOM||'AETEEMBAUCHEUN'||TO_CHAR(EMBAUCHE,'DAY')                
--------------------------------------------------------------------------------
King                                                                            
James                                                                           
King James a ete embauche un Jeudi                                              
                                                                                
Jones                                                                           
Bob                                                                             
Jones Bob a ete embauche un Vendredi                                            

EMPNOM                                                                          
--------------------------------------------------                              
EMPPRENOM                                                                       
--------------------------------------------------                              
EMPNOM||''||EMPPRENOM||'AETEEMBAUCHEUN'||TO_CHAR(EMBAUCHE,'DAY')                
--------------------------------------------------------------------------------
                                                                                
Ford                                                                            
Tom                                                                             
Ford Tom a ete embauche un Mercredi                                             
                                                                                
Chabot                                                                          
                                                                                

EMPNOM                                                                          
--------------------------------------------------                              
EMPPRENOM                                                                       
--------------------------------------------------                              
EMPNOM||''||EMPPRENOM||'AETEEMBAUCHEUN'||TO_CHAR(EMBAUCHE,'DAY')                
--------------------------------------------------------------------------------
Chabot  a ete embauche un                                                       
                                                                                
Ford                                                                            
Tom                                                                             
Ford Tom a ete embauche un Mardi                                                
                                                                                
Smith                                                                           

EMPNOM                                                                          
--------------------------------------------------                              
EMPPRENOM                                                                       
--------------------------------------------------                              
EMPNOM||''||EMPPRENOM||'AETEEMBAUCHEUN'||TO_CHAR(EMBAUCHE,'DAY')                
--------------------------------------------------------------------------------
Julia                                                                           
Smith Julia a ete embauche un Mardi                                             
                                                                                
Blake                                                                           
Tom                                                                             
Blake Tom a ete embauche un Mardi                                               
                                                                                

7 rows selected.

SQL> SELECT MAX(SALAIRE), MIN(SALAIRE) FROM Employes;

MAX(SALAIRE) MIN(SALAIRE)                                                       
------------ ------------                                                       
   286574.55        65699                                                       

SQL> SELECT DISTINCT SUPERVISEUR FROM Employes;

SUPERVISEUR                                                                     
-----------                                                                     
                                                                                
       7893                                                                     
       7902                                                                     
       7566                                                                     

SQL> SELECT EMPNOM FROM Employes WHERE EMPNOM = 'Ford';

EMPNOM                                                                          
--------------------------------------------------                              
Ford                                                                            
Ford                                                                            

SQL> SELECT EMPNO, EMPNOM, EMPPRENOM FROM Employes WHERE EMPNOM = 'Ford';

     EMPNO EMPNOM                                                               
---------- --------------------------------------------------                   
EMPPRENOM                                                                       
--------------------------------------------------                              
      7902 Ford                                                                 
Tom                                                                             
                                                                                
      7934 Ford                                                                 
Tom                                                                             
                                                                                

SQL> SELECT EMPNO, EMPNOM, EMPPRENOM FROM Employes WHERE SUPERVISEUR = '7893';

     EMPNO EMPNOM                                                               
---------- --------------------------------------------------                   
EMPPRENOM                                                                       
--------------------------------------------------                              
      7566 Jones                                                                
Bob                                                                             
                                                                                
      7698 Blake                                                                
Tom                                                                             
                                                                                

SQL> SELECT EMPNO, EMPNOM, EMPPRENOM FROM Employes WHERE DEPNO = 'OPE';

     EMPNO EMPNOM                                                               
---------- --------------------------------------------------                   
EMPPRENOM                                                                       
--------------------------------------------------                              
      7893 King                                                                 
James                                                                           
                                                                                
      7369 Smith                                                                
Julia                                                                           
                                                                                

SQL> SELECT EMPNO, EMPNOM, EMPPRENOM FROM Employes WHERE CODEPOSTAL IS NULL;

     EMPNO EMPNOM                                                               
---------- --------------------------------------------------                   
EMPPRENOM                                                                       
--------------------------------------------------                              
      7845 Chabot                                                               
                                                                                
                                                                                
      7369 Smith                                                                
Julia                                                                           
                                                                                
      7698 Blake                                                                
Tom                                                                             
                                                                                

SQL> SELECT EMPNO, EMPNOM, EMPPRENOM FROM Employes WHERE VILLE IS NOT NULL;

     EMPNO EMPNOM                                                               
---------- --------------------------------------------------                   
EMPPRENOM                                                                       
--------------------------------------------------                              
      7893 King                                                                 
James                                                                           
                                                                                
      7566 Jones                                                                
Bob                                                                             
                                                                                
      7902 Ford                                                                 
Tom                                                                             
                                                                                

     EMPNO EMPNOM                                                               
---------- --------------------------------------------------                   
EMPPRENOM                                                                       
--------------------------------------------------                              
      7934 Ford                                                                 
Tom                                                                             
                                                                                

SQL> SELECT TABLE_NAME FROM USER_TABLES;

TABLE_NAME                                                                      
--------------------------------------------------------------------------------
EMPLOYES                                                                        
CONTACT                                                                         

SQL> DELETE FROM Employes;

7 rows deleted.

SQL> SELECT * FROM Employes;

no rows selected

SQL> DROP TABLE Employes;

Table dropped.

SQL> SPOOL OFF;
