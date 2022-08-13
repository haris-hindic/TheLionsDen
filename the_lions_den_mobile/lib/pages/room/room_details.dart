import 'package:carousel_slider/carousel_slider.dart';
import 'package:flutter/material.dart';
import 'package:provider/provider.dart';
import 'package:the_lions_den_mobile/model/room/room_response.dart';
import 'package:the_lions_den_mobile/model/room_image/room_image_response.dart';
import 'package:the_lions_den_mobile/providers/room_provider.dart';
import 'package:the_lions_den_mobile/utils/util.dart';

class RoomDetails extends StatefulWidget {
  static const String routeName = "/room/{id}";

  const RoomDetails({Key? key}) : super(key: key);

  @override
  State<RoomDetails> createState() => _RoomDetailsState();
}

class _RoomDetailsState extends State<RoomDetails> {
  RoomProvider? _roomProvider;
  RoomResponse data = RoomResponse();
  List<RoomImageResponse> images = [];

  @override
  void initState() {
    super.initState();
    _roomProvider = context.read<RoomProvider>();
    loadData();
  }

  Future loadData() async {
    var tempData = await _roomProvider?.getById(4);
    setState(() {
      data = tempData!;
      images = tempData.roomType!.roomImages!;
    });
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(body: _buildDetails());
  }

  _buildDetails() {
    if (data.coverImage == null) {
      return const Text("Loading.....");
    }

    return SafeArea(
      child: Stack(
        children: <Widget>[
          CarouselSlider(
            options: CarouselOptions(
              height: 350,
              autoPlay: true,
              autoPlayInterval: Duration(seconds: 1),
              autoPlayAnimationDuration: Duration(milliseconds: 800),
              autoPlayCurve: Curves.fastOutSlowIn,
              enableInfiniteScroll: true,
            ),
            items: images
                .map((e) => Container(
                    width: 500,
                    foregroundDecoration: BoxDecoration(
                        color: Colors.black26,
                        border: Border.all(color: Colors.grey, width: 1)),
                    child: Image.memory(
                      dataFromBase64String(e.image as String),
                      fit: BoxFit.fill,
                    )))
                .toList(),
          ),
          SingleChildScrollView(
            padding: const EdgeInsets.only(top: 16.0, bottom: 20.0),
            child: Column(
              crossAxisAlignment: CrossAxisAlignment.start,
              children: <Widget>[
                const SizedBox(height: 250),
                Padding(
                  padding: const EdgeInsets.symmetric(horizontal: 16.0),
                  child: Text(
                    data.name!,
                    style: TextStyle(
                        color: Colors.white,
                        fontSize: 28.0,
                        fontWeight: FontWeight.bold),
                  ),
                ),
                Row(
                  children: <Widget>[
                    const SizedBox(width: 16.0),
                    Container(
                      padding: const EdgeInsets.symmetric(
                        vertical: 8.0,
                        horizontal: 16.0,
                      ),
                      decoration: BoxDecoration(
                          color: Colors.grey,
                          borderRadius: BorderRadius.circular(20.0)),
                      child: Text(
                        data.roomTypeName!,
                        style: TextStyle(color: Colors.white, fontSize: 13.0),
                      ),
                    ),
                    Spacer(),
                    IconButton(
                      color: Colors.white,
                      icon: Icon(Icons.favorite_border),
                      onPressed: () {},
                    )
                  ],
                ),
                Container(
                  padding: const EdgeInsets.all(32.0),
                  color: Colors.white,
                  child: Column(
                    crossAxisAlignment: CrossAxisAlignment.start,
                    mainAxisSize: MainAxisSize.min,
                    children: <Widget>[
                      Row(
                        mainAxisAlignment: MainAxisAlignment.center,
                        children: <Widget>[
                          Text(
                            "\$ ${data.price}",
                            style: TextStyle(
                                color: Color.fromARGB(255, 26, 60, 117),
                                fontWeight: FontWeight.bold,
                                fontSize: 22.0),
                          ),
                          SizedBox(
                            width: 5,
                          ),
                          Text(
                            "/per night",
                            style:
                                TextStyle(fontSize: 14.0, color: Colors.grey),
                          )
                        ],
                      ),
                      const SizedBox(height: 20.0),
                      Container(
                        height: 50,
                        width: 400,
                        decoration: BoxDecoration(
                            borderRadius: BorderRadius.circular(50),
                            gradient: const LinearGradient(colors: [
                              Color.fromARGB(255, 0, 69, 189),
                              Color.fromARGB(255, 10, 158, 227)
                            ])),
                        child: InkWell(
                          child: const Center(child: Text("Book Now!")),
                        ),
                      ),
                      const SizedBox(height: 30.0),
                      Text(
                        "Description".toUpperCase(),
                        style: TextStyle(
                            fontWeight: FontWeight.w600, fontSize: 14.0),
                      ),
                      const SizedBox(height: 10.0),
                      Text(
                        data.roomType!.description!,
                        textAlign: TextAlign.justify,
                        style: TextStyle(
                            fontWeight: FontWeight.w300, fontSize: 14.0),
                      ),
                      const SizedBox(height: 30.0),
                      Text(
                        "Rules".toUpperCase(),
                        style: TextStyle(
                            fontWeight: FontWeight.w600, fontSize: 14.0),
                      ),
                      const SizedBox(height: 10.0),
                      Text(
                        data.roomType!.rules!,
                        textAlign: TextAlign.justify,
                        style: TextStyle(
                            fontWeight: FontWeight.w300, fontSize: 14.0),
                      ),
                      const SizedBox(height: 30.0),
                      Text(
                        "Amenities".toUpperCase(),
                        style: TextStyle(
                            fontWeight: FontWeight.w600, fontSize: 14.0),
                      ),
                      const SizedBox(height: 10.0),
                      Container(
                        height: 75,
                        child: GridView(
                          gridDelegate:
                              SliverGridDelegateWithFixedCrossAxisCount(
                                  crossAxisCount: 1,
                                  childAspectRatio: 1,
                                  crossAxisSpacing: 20,
                                  mainAxisSpacing: 30),
                          scrollDirection: Axis.horizontal,
                          children: _buildAmenitesGrid(),
                        ),
                      ),
                    ],
                  ),
                ),
              ],
            ),
          ),
          Positioned(
            top: 0,
            left: 0,
            right: 0,
            child: AppBar(
              backgroundColor: Colors.transparent,
              elevation: 0,
              centerTitle: true,
              title: Text(
                "ROOM DETAILS",
                style: TextStyle(fontSize: 16.0, fontWeight: FontWeight.normal),
              ),
            ),
          ),
        ],
      ),
    );
  }

  List<Widget> _buildAmenitesGrid() {
    if (data.amenities!.isEmpty) {
      return [const Text("No amenities...")];
    }

    List<Widget> list = data.roomAmenities!
        .map(
          (e) => Container(
            child: ClipOval(
              child: Material(
                child: Container(
                  child: Row(
                    mainAxisAlignment: MainAxisAlignment.spaceEvenly,
                    children: <Widget>[
                      Icon(
                        Icons.add_reaction_outlined,
                        color: Color.fromARGB(255, 10, 158, 227),
                      ),
                      Text(e.amenityName!),
                    ],
                  ),
                ),
              ),
            ),
          ),
        )
        .cast<Widget>()
        .toList();

    list.addAll(data.roomAmenities!
        .map(
          (e) => Container(
            child: ClipOval(
              child: Material(
                child: Container(
                  child: Row(
                    mainAxisAlignment: MainAxisAlignment.spaceEvenly,
                    children: <Widget>[
                      Icon(
                        Icons.add_reaction_outlined,
                        color: Color.fromARGB(255, 10, 158, 227),
                      ),
                      Text(e.amenityName!),
                    ],
                  ),
                ),
              ),
            ),
          ),
        )
        .cast<Widget>()
        .toList());

    return list;
  }
}
