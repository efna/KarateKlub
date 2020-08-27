<?php

  if (isset($_POST["departament_id"])) {
    $departament_id = $_POST['departament_id'];

  try{
  ini_set('soap.wsdl_cache_enabled',0);
  ini_set('soap.wsdl_cache_ttl',0);

    $sClient = new SoapClient('http://localhost:81/web_servis_php/profesori.wsdl',
              array('cache_wsdl'=>WSDL_CACHE_NONE,'trace' => 1)
              );
    $response = $sClient->listProfesori($departament_id);
    //var_dump($sClient->__getLastRequest()); //Ako treba ispisat SOAP req objekt, za resp _getLastResponse
    $sClient->__getLastRequest();

  }catch(SoapFault $e){
    var_dump($e);
    echo $e;
  }
}
?>
<!DOCTYPE html> 
<html lang="en">
<head>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css">
    <link rel="stylesheet" type="text/css" href="css/university.css">
    <title>University</title>
</head>
<body>

    <div class="col-12 header">
      <h1 class="col-8">University</h1>
      <p class="col-4 logo">  </p>
    </div>
    
    <div class="row">
      <div class="col-12 menu">
        <ul>
          <li class="topnav" id="myTopnav">
            <a href="index.html">Početna</a>
            <a href="javascript:void(0);" class="icon" onclick="navBarResponzive()"> </li>
          <li>
            <a href="profesori.html" class="active">Profesori</a>
            <a href="javascript:void(0);" class="icon" onclick="navBarResponzive()"> </li>
         </ul>
      </div>

        <div class="col-9">
          <div class="main_frame">
            <div class="search_form">
            <p>Upišite ID odjela:</p>  
              <form id="form" action="./client.php" method="post">
                 <input type="text" id="departament_id" name="departament_id">  
                  <button type="submit">Pretraži</button>
                </form>
            </div>
            <div class="frame1">
                  <?php
		                for($j=0;$j<sizeof($response);$j++) {
                          echo "<hr><div class='profesori'>
                                <p><i>Profesor:</i> <b><i>".$response[$j][0]."</i></b></p>
                                <p><i>Odjel:</i> <b><i>".$response[$j][1]."</i></b></p>
                                <p><i>Titula:</i> <b><i>".$response[$j][2]."</i></b></p>
                                <p><i>Fakultet:</i> <b><i>".$response[$j][3]."</i></b></p>
                                </div>";
					            }
				            ?>
              </div>
            </div>
      </div>
    </div>
    <div class="footer">
      <p>Razvoj naprednih web aplikacija - Tim 12 (Efnan Dugalić i Sanja Tinjak) 2019-2020</p>
    </div>

    <script src="./javascrip.js"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
    </body>
</html>
