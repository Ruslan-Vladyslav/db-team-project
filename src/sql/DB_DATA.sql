SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;

-- -----------------------------------------------------
-- Data for table `OpenDataModel`.`Categoty`
-- -----------------------------------------------------
START TRANSACTION;
USE `OpenDataModel`;
INSERT INTO `OpenDataModel`.`Categoty` (`category_id`, `name`, `parent_category_id`) VALUES (DEFAULT, 'Географія', NULL);
INSERT INTO `OpenDataModel`.`Categoty` (`category_id`, `name`, `parent_category_id`) VALUES (DEFAULT, 'Статистика', NULL);

COMMIT;



-- -----------------------------------------------------
-- Data for table `OpenDataModel`.`Tag`
-- -----------------------------------------------------
START TRANSACTION;
USE `OpenDataModel`;
INSERT INTO `OpenDataModel`.`Tag` (`tag_id`, `name`) VALUES (DEFAULT, 'Тег: статистика');
INSERT INTO `OpenDataModel`.`Tag` (`tag_id`, `name`) VALUES (DEFAULT, 'Тег: географія');

COMMIT;


-- -----------------------------------------------------
-- Data for table `OpenDataModel`.`Data`
-- -----------------------------------------------------
START TRANSACTION;
USE `OpenDataModel`;
INSERT INTO `OpenDataModel`.`Data` (`data_id`, `name`, `description`, `format`, `content`, `createdAt`, `updatedAt`, `category_id`) VALUES (DEFAULT, 'Статистика', 'Важлива статистика', 'txt', 'txt', '2038-01-19 03:14:07', '2039-01-19 03:14:07', 1);
INSERT INTO `OpenDataModel`.`Data` (`data_id`, `name`, `description`, `format`, `content`, `createdAt`, `updatedAt`, `category_id`) VALUES (DEFAULT, 'Географія', 'Важливі дані', 'png', 'png', '2027-01-19 03:14:07', '2030-01-19 03:14:07', 2);

COMMIT;


-- -----------------------------------------------------
-- Data for table `OpenDataModel`.`Link`
-- -----------------------------------------------------
START TRANSACTION;
USE `OpenDataModel`;
INSERT INTO `OpenDataModel`.`Link` (`link_id`, `data_id`, `tag_id`) VALUES (DEFAULT, 1, 1);
INSERT INTO `OpenDataModel`.`Link` (`link_id`, `data_id`, `tag_id`) VALUES (DEFAULT, 2, 2);

COMMIT;


-- -----------------------------------------------------
-- Data for table `OpenDataModel`.`Role`
-- -----------------------------------------------------
START TRANSACTION;
USE `OpenDataModel`;
INSERT INTO `OpenDataModel`.`Role` (`role_id`, `name`) VALUES (DEFAULT, 'Користувач');
INSERT INTO `OpenDataModel`.`Role` (`role_id`, `name`) VALUES (DEFAULT, 'Адміністратор');

COMMIT;


-- -----------------------------------------------------
-- Data for table `OpenDataModel`.`User`
-- -----------------------------------------------------
START TRANSACTION;
USE `OpenDataModel`;
INSERT INTO `OpenDataModel`.`User` (`user_id`, `firstname`, `lastname`, `email`, `login`, `password`) VALUES (DEFAULT, 'Іван', 'Рєзник', 'rieznyk@gmail.com', 'ivan', '1234');
INSERT INTO `OpenDataModel`.`User` (`user_id`, `firstname`, `lastname`, `email`, `login`, `password`) VALUES (DEFAULT, 'Нікіта', 'Пляко', 'plyako@gmail.com', 'nikito4ka', '5678');

COMMIT;


-- -----------------------------------------------------
-- Data for table `OpenDataModel`.`Access`
-- -----------------------------------------------------
START TRANSACTION;
USE `OpenDataModel`;
INSERT INTO `OpenDataModel`.`Access` (`access_id`, `data_id`, `role_id`, `user_id`) VALUES (DEFAULT, 1, 1, 1);
INSERT INTO `OpenDataModel`.`Access` (`access_id`, `data_id`, `role_id`, `user_id`) VALUES (DEFAULT, 2, 2, 2);

COMMIT;