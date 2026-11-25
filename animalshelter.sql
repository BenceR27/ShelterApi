-- phpMyAdmin SQL Dump
-- version 5.2.3
-- https://www.phpmyadmin.net/
--
-- Gép: mariadb:3306
-- Létrehozás ideje: 2026. Nov 25. 07:55
-- Kiszolgáló verziója: 11.8.9-MariaDB-ubu2404
-- PHP verzió: 8.3.33

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Adatbázis: `animalshelter`
--

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `animals`
--

CREATE TABLE `animals` (
  `Id` char(36) NOT NULL,
  `Name` varchar(40) NOT NULL,
  `Species` varchar(30) NOT NULL,
  `Age` int(11) NOT NULL,
  `Weight` double NOT NULL,
  `RegTime` datetime NOT NULL DEFAULT current_timestamp(),
  `ShelterId` char(36) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- A tábla adatainak kiíratása `animals`
--

INSERT INTO `animals` (`Id`, `Name`, `Species`, `Age`, `Weight`, `RegTime`, `ShelterId`) VALUES
('e23cf47a-e89c-11f1-a28c-fa7223c4431a', 'Bodri', 'Kutya', 3, 18.5, '2026-11-25 06:53:13', 'dd87a9dd-de9b-55c7-8fa6-fdafdfcc4a64'),
('e23d0463-e89c-11f1-a28c-fa7223c4431a', 'Cirmi', 'Macska', 2, 4.2, '2026-11-25 06:53:13', '95f1187f-f2c2-54b7-afc1-3dba453580ab'),
('e23d0618-e89c-11f1-a28c-fa7223c4431a', 'Rex', 'Kutya', 5, 28, '2026-11-25 06:53:13', '8d32af8e-bca2-5907-997f-05ff3b325fe7'),
('e23d066d-e89c-11f1-a28c-fa7223c4431a', 'Mici', 'Nyúl', 1, 1.8, '2026-11-25 06:53:13', 'a2691474-e139-594f-a782-443415ee7d41'),
('e23d0699-e89c-11f1-a28c-fa7223c4431a', 'Morzsi', 'Kutya', 4, 12.3, '2026-11-25 06:53:13', 'c41845d3-62ac-5c31-9b16-80193b82aec0');

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `shelters`
--

CREATE TABLE `shelters` (
  `Id` char(36) NOT NULL,
  `Name` varchar(50) NOT NULL,
  `City` varchar(40) NOT NULL,
  `Capacity` int(11) NOT NULL,
  `RegTime` datetime DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- A tábla adatainak kiíratása `shelters`
--

INSERT INTO `shelters` (`Id`, `Name`, `City`, `Capacity`, `RegTime`) VALUES
('8d32af8e-bca2-5907-997f-05ff3b325fe7', 'Gazdira Vár', 'Debrecen', 25, '2026-08-05 08:04:01'),
('95f1187f-f2c2-54b7-afc1-3dba453580ab', 'Cirmos Menedék', 'Eger', 15, '2026-07-18 11:51:48'),
('a2691474-e139-594f-a782-443415ee7d41', 'Boldog Mancs', 'Szeged', 40, '2026-01-08 15:34:23'),
('c41845d3-62ac-5c31-9b16-80193b82aec0', 'Kis Menedék', 'Salgótarján', 3, '2026-05-25 09:52:06'),
('dd87a9dd-de9b-55c7-8fa6-fdafdfcc4a64', 'Négylábú Otthon', 'Miskolc', 30, '2026-04-10 08:46:25');

--
-- Indexek a kiírt táblákhoz
--

--
-- A tábla indexei `animals`
--
ALTER TABLE `animals`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `FK_Animals_Shelters` (`ShelterId`);

--
-- A tábla indexei `shelters`
--
ALTER TABLE `shelters`
  ADD PRIMARY KEY (`Id`);

--
-- Megkötések a kiírt táblákhoz
--

--
-- Megkötések a táblához `animals`
--
ALTER TABLE `animals`
  ADD CONSTRAINT `FK_Animals_Shelters` FOREIGN KEY (`ShelterId`) REFERENCES `shelters` (`Id`) ON DELETE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
