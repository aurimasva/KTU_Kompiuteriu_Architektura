------------------------------------- 

--KTU 2015

--Informatikos fakultetas
--Kompiuteriu katedra
--Kompiuteriu Architektura [P175B125] 
--Kazimieras Bagdonas 

--v1.0

------------------------------------- 
--KTU 2016 

--ditto

--v1.01
--panaikinta "save" mikrokomanda registrams, sutrumpinta ROM eilute nuo 75 iki 69 bitu, nesuderinama su V1.0   

------------------------------------- 

library ieee;
use ieee.std_logic_1164.all;
use ieee.numeric_std.all;

entity ROM is 
	port (
		RST_ROM : in std_logic;
		ROM_CMD : in std_logic_vector(7 downto 0);  
		ROM_Dout : out std_logic_vector(1 to 69)
		);
end ROM ;

architecture rtl of ROM is
	
	type memory is array (0 to 255) of std_logic_vector(1 to 69) ; 
	
	constant ROM_CMDln : memory := (  
--                    1         2         3         4         5         6            Dvi komentaro eilutes duoda bitu numerius   
--           123456789012345678901234567890123456789012345678901234567890123456789    (nuo 1 iki 69)
	0=> "010000000000000100000000000000000000000000000000000000000000000000000",
	1=> "000000000000000000000010000000000000000000000000000000000000000000000",  --Komentaro vieta
	2=> "000010000000000000000000000000000000000000000000000000000000000000000",  --Komentaro vieta
	3=> "100110000011000000000000000000000000000000000000000000000000000000000",  --Komentaro vieta
	4=> "000000001000000000000000000000000000000000000000001000000000000000000",  --Komentaro vieta
	5=> "000000000000000000000000000000000000000000000000000000000000000000000",  --Komentaro vieta
	6=> "111010000100100000000000000000000000000000000000000000000000000000000",  --Komentaro vieta
	7=> "001000000000000000000000000000000000000000000000000000000000000000000",  --Komentaro vieta
	8=> "000000000000000000000000000000000000000000000000000000000001100010110",  --Komentaro vieta
	9=> "000000000100000010000000000000000000000000000000000000000000000000001",  --Komentaro vieta
	10=> "111110000001100000000000000000000000000000000000000000000000000000000",  --Komentaro vieta
	
	others => (others => '0') );   
	
	
	
begin
	process (RST_ROM, ROM_CMD) 
		
	begin
		if RST_ROM'event and RST_ROM = '1' then 
			ROM_Dout <= ROM_CMDln(0);
		elsif ROM_CMD'event then
			ROM_Dout <= ROM_CMDln(to_integer(unsigned(ROM_CMD))); 
		end if;
		
	end process;
	
end rtl;