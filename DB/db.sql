-- MySQL dump 10.13  Distrib 8.0.36, for Win64 (x86_64)
--
-- Host: localhost    Database: db_1
-- ------------------------------------------------------
-- Server version	8.0.37

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `categories`
--

DROP TABLE IF EXISTS `categories`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `categories` (
  `id` int NOT NULL AUTO_INCREMENT,
  `title` varchar(100) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=43 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `categories`
--

LOCK TABLES `categories` WRITE;
/*!40000 ALTER TABLE `categories` DISABLE KEYS */;
INSERT INTO `categories` VALUES (1,'Сыры'),(2,'Мясо и птица'),(3,'Колбаса и сосиски'),(4,'Замороженные продукты'),(5,'Здоровый выбор'),(6,'Сладости'),(7,'Овощи и зелень');
/*!40000 ALTER TABLE `categories` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `product`
--

DROP TABLE IF EXISTS `product`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `product` (
  `idproduct` int unsigned NOT NULL AUTO_INCREMENT,
  `title` varchar(200) NOT NULL,
  `categories_id` int NOT NULL,
  `title_path` varchar(100) DEFAULT NULL,
  `image` longblob,
  PRIMARY KEY (`idproduct`),
  UNIQUE KEY `idproduct_UNIQUE` (`idproduct`),
  KEY `fk_product_categories_idx` (`categories_id`),
  CONSTRAINT `fk_product_categories` FOREIGN KEY (`categories_id`) REFERENCES `categories` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=70 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `product`
--

LOCK TABLES `product` WRITE;
/*!40000 ALTER TABLE `product` DISABLE KEYS */;
INSERT INTO `product` VALUES (1,'Сыр Viola Бутербродный полутвердый нарезка 45% БЗМЖ 120г',1,'\\Resources\\cheese_1.jpg',NULL),(2,'Сыр Свежий Ряд российский нарезка 50% БЗМЖ 150г',1,'\\Resources\\cheese_2.jpeg',NULL),(3,'Сыр Liebendorf Гауда полутвердый нарезка БЗМЖ 150г',1,'\\Resources\\cheese_3.jpeg',NULL),(4,'Напиток овсяный Nemoloko шоколадный 3.2% 1л',5,'\\Resources\\napitok-ovsyanyy.jpg',NULL),(5,'Напиток овсяный Nemoloko классический 3.2% 1л',5,'\\Resources\\Nemoloko_1L.jpg',NULL),(6,'Лед Завод Льда пищевой 1кг',4,'\\Resources\\no_images.png',NULL),(7,'Пломбир Золотой Стандарт С Таежной Черникой с черничным наполнителем в вафельном стаканчике 12% БЗМЖ 93г',4,'\\Resources\\no_images.png',NULL),(8,'Бекон Мясная Ферма сырокопченый 150г',3,'\\Resources\\2355319_51712017.jpeg',NULL),(9,'Колбаса Черкизово Сальчичон сырокопченая нарезка 100г',3,'\\Resources\\17a30a6bf4add77ac76f83b247bd9922.w700h700.jpeg',NULL),(10,'Тушка цыпленка охлажденная',2,'\\Resources\\e852b61256a784e57ea92105b1355131.jpg',NULL),(11,'Филе куриное охлажденное',2,'\\Resources\\4b06ee9a489cebf9ef15956b12787717.jpg',NULL);
/*!40000 ALTER TABLE `product` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `user`
--

DROP TABLE IF EXISTS `user`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `user` (
  `idUser` int NOT NULL AUTO_INCREMENT,
  `login` varchar(50) NOT NULL,
  `email` varchar(50) NOT NULL,
  `password` varchar(200) NOT NULL,
  PRIMARY KEY (`idUser`),
  UNIQUE KEY `email_UNIQUE` (`email`)
) ENGINE=InnoDB AUTO_INCREMENT=115 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `user`
--

LOCK TABLES `user` WRITE;
/*!40000 ALTER TABLE `user` DISABLE KEYS */;
INSERT INTO `user` VALUES (11,'123','123','A6-65-A4-59-20-42-2F-9D-41-7E-48-67-EF-DC-4F-B8-A0-4A-1F-3F-FF-1F-A0-7E-99-8E-86-F7-F7-A2-7A-E3'),(12,'123','1234123','A6-65-A4-59-20-42-2F-9D-41-7E-48-67-EF-DC-4F-B8-A0-4A-1F-3F-FF-1F-A0-7E-99-8E-86-F7-F7-A2-7A-E3'),(13,'123','1231','A6-65-A4-59-20-42-2F-9D-41-7E-48-67-EF-DC-4F-B8-A0-4A-1F-3F-FF-1F-A0-7E-99-8E-86-F7-F7-A2-7A-E3'),(113,'1111','1111','0F-FE-1A-BD-1A-08-21-53-53-C2-33-D6-E0-09-61-3E-95-EE-C4-25-38-32-A7-61-AF-28-FF-37-AC-5A-15-0C'),(114,'1234','DELETE FROM `db_1`.`user` WHERE (`idUser` = \'113\')','03-AC-67-42-16-F3-E1-5C-76-1E-E1-A5-E2-55-F0-67-95-36-23-C8-B3-88-B4-45-9E-13-F9-78-D7-C8-46-F4');
/*!40000 ALTER TABLE `user` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2024-12-14 11:29:17
