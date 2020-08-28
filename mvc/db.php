<?php
$dsn = 'mysql:host=localhost;dbname=testing';
$username = 'root';
$password = 'Dalia0909';
$options = [];
try {
$connection = new PDO($dsn, $username, $password, $options);
} catch(PDOException $e) {

}
